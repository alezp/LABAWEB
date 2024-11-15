﻿using Fantasy.Frontend.Repositories;
using System.Text.Json;
using System.Text;

public async Task<HttpResponseWrapper<object>> DeleteAsync(string url)
{
    var responseHttp = await _httpClient.DeleteAsync(url);
    return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
}

public async Task<HttpResponseWrapper<object>> PutAsync<T>(string url, T model)
{
    var messageJson = JsonSerializer.Serialize(model);
    var messageContent = new StringContent(messageJson, Encoding.UTF8, "application/json");
    var responseHttp = await _httpClient.PutAsync(url, messageContent);
    return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
}

public async Task<HttpResponseWrapper<TActionResponse>> PutAsync<T, TActionResponse>(string url, T model)
{
    var messageJson = JsonSerializer.Serialize(model);
    var messageContent = new StringContent(messageJson, Encoding.UTF8, "application/json");
    var responseHttp = await _httpClient.PutAsync(url, messageContent);
    if (responseHttp.IsSuccessStatusCode)
    {
        var response = await UnserializeAnswerAsync<TActionResponse>(responseHttp);
        return new HttpResponseWrapper<TActionResponse>(response, false, responseHttp);
    }
    return new HttpResponseWrapper<TActionResponse>(default, true, responseHttp);
}
