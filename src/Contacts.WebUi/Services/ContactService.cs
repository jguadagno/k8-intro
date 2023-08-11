using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Contacts.WebUi.Models;

namespace Contacts.WebUi.Services;

public class ContactService: IContactService
{
    private readonly HttpClient _httpClient;
    private readonly Settings _settings;

    public ContactService(HttpClient httpClient, Settings settings)
    {
        _httpClient = httpClient;
        _settings = settings;
    }

    public async Task<Domain.Models.Contact> GetContactAsync(int contactId)
    {
        var url = $"{_settings.ApiRootUri}contacts/{contactId}";
        return await ExecuteGetAsync<Domain.Models.Contact>(url);
    }

    public async Task<List<Domain.Models.Contact>> GetContactsAsync()
    {
        var url = $"{_settings.ApiRootUri}contacts";
        return await ExecuteGetAsync<List<Domain.Models.Contact>>(url);
    }

    public async Task<List<Domain.Models.Contact>> GetContactsAsync(string firstName, string lastName)
    {
        var url = $"{_settings.ApiRootUri}contacts/search?firstname={firstName}&lastname={lastName}";
        return await ExecuteGetAsync<List<Domain.Models.Contact>>(url);
    }

    public async Task<Domain.Models.Contact> SaveContactAsync(Domain.Models.Contact contact)
    {
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var url = $"{_settings.ApiRootUri}contacts/";
        var jsonRequest = JsonSerializer.Serialize(contact);
        var jsonContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(url, jsonContent);

        if (response.StatusCode != HttpStatusCode.Created)
            throw new HttpRequestException(
                $"Invalid status code in the HttpResponseMessage: {response.StatusCode}.");
            
        var content = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
        contact = JsonSerializer.Deserialize<Domain.Models.Contact>(content, options);
        return contact;
    }

    public async Task<bool> DeleteContactAsync(Domain.Models.Contact contact)
    {
        return await DeleteContactAsync(contact.ContactId);
    }

    public async Task<bool> DeleteContactAsync(int contactId)
    {
        var url = $"{_settings.ApiRootUri}contacts/{contactId}";
        var response = await _httpClient.DeleteAsync(url);
        return response.StatusCode == HttpStatusCode.NoContent;
    }

    public async Task<Domain.Models.Phone> GetContactPhoneAsync(int contactId, int phoneId)
    {
        var url = $"{_settings.ApiRootUri}contacts/{contactId}/phones/{phoneId}";
        return await ExecuteGetAsync<Domain.Models.Phone>(url);
    }

    public async Task<List<Domain.Models.Phone>> GetContactPhonesAsync(int contactId)
    {
        var url = $"{_settings.ApiRootUri}contacts/{contactId}/phones";
        return await ExecuteGetAsync<List<Domain.Models.Phone>>(url);
    }
        
    public async Task<Domain.Models.Address> GetContactAddressAsync(int contactId, int addressId)
    {
        var url = $"{_settings.ApiRootUri}contacts/{contactId}/addresses/{addressId}";
        return await ExecuteGetAsync<Domain.Models.Address>(url);
    }

    public async Task<List<Domain.Models.Address>> GetContactAddressesAsync(int contactId)
    {
        var url = $"{_settings.ApiRootUri}contacts/{contactId}/addresses";
        return await ExecuteGetAsync<List<Domain.Models.Address>>(url);
    }
        
    private async Task<T> ExecuteGetAsync<T>(string url)
    {
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var response = await _httpClient.GetAsync(url);
        if (response.StatusCode != HttpStatusCode.OK)
            throw new HttpRequestException(
                $"Invalid status code in the HttpResponseMessage: {response.StatusCode}.");
            
        // Parse the Results
        var content = await response.Content.ReadAsStringAsync();
                
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        var results = JsonSerializer.Deserialize<T>(content, options);

        return results;
    }
}