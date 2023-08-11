﻿using System.Net.Http.Json;
using System.Text.Json;

using KeyvanSafe.Shared.Infrastructure.Pagination;

namespace KeyvanSafe.Shared.Persistence.HttpObjects;
public class HttpService : IHttpService
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _options;

    public HttpService(HttpClient client)
    {
        _client = client;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    #region Get

    public async Task<T> GetValue<T>(string requestUrl)
    {
        var response = await _client.GetAsync(requestUrl);

        var content = await response.Content.ReadAsStreamAsync();
        if (!response.IsSuccessStatusCode)
            throw new Exception("bad request | maybe wrong address");

        return await JsonSerializer.DeserializeAsync<T>(content, _options);
    }

    public async Task<List<T>> GetValueList<T>(string requestUrl)
    {
        var response = await _client.GetAsync(requestUrl);

        var content = await response.Content.ReadAsStreamAsync();
        if (!response.IsSuccessStatusCode)
            throw new Exception("bad request | maybe wrong address");

        return await JsonSerializer.DeserializeAsync<List<T>>(content, _options);
    }

    #endregion

    #region Post Put 

    public async Task<HttpResponseMessage> PostValue<T>(string requestUrl, T data)
    {
        try
        {
            var serializedData = JsonSerializer.Serialize(data, _options);
            var response = await _client.PostAsJsonAsync(requestUrl, serializedData);
            return response;
        }
        catch (Exception e)
        {

            throw new Exception(e.Message, e);
        }
    }

    public async Task<HttpResponseMessage> PutValue<T>(string requestUrl, T data)
    {
        var serializedData = JsonSerializer.Serialize(data, _options);
        return await _client.PutAsJsonAsync(requestUrl, serializedData);
    }

    #endregion

    #region Delete

    public async Task<HttpResponseMessage> DeleteValue<T>(string requestUrl)
    {
        return await _client.DeleteAsync(requestUrl);
    }

    #endregion

    #region Pagination

    public async Task<PaginatedList<T>> GetPagedValue<T>(string requestUrl, DefaultPaginationFilter defaultPaginationFilter)
    {
        var serializedData = await Task.Run(() => JsonSerializer.Serialize(defaultPaginationFilter, _options));
        var response = await _client.PostAsJsonAsync(requestUrl, serializedData);
        var dataAsJson = await response.Content.ReadAsStreamAsync();
        var data = await JsonSerializer.DeserializeAsync<PaginatedList<T>>(dataAsJson, _options);

        return data ?? throw new NullReferenceException();
    }

    #endregion
}