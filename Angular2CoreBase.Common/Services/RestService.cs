﻿namespace Angular2CoreBase.Common.Services
{
	//https://github.com/NimaAra/Easy.Common/tree/master/Easy.Common

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net.Http;
	using System.Threading;
	using System.Threading.Tasks;
	using Interfaces;
	using WeatherServices;

	/// <summary>
	/// An abstraction over <see cref="HttpClient"/> to address the following issues:
	/// <para><see href="http://aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/"/></para>
	/// <para><see href="http://byterot.blogspot.co.uk/2016/07/singleton-httpclient-dns.html"/></para>
	/// <para><see href="http://www.nimaara.com/2016/11/01/beware-of-the-net-httpclient/"/></para>
	/// </summary>
	public sealed class RestService : IRestService
	{
		private readonly HttpClient _client;
		private readonly HashSet<Uri> _endpoints;

		/// <summary>
		/// Creates an instance of the <see cref="RestService"/>.
		/// </summary>
		public RestService(
			IDictionary<string, string> defaultRequestHeaders = null,
			HttpMessageHandler handler = null,
			bool disposeHandler = true,
			TimeSpan? timeout = null,
			ulong? maxResponseContentBufferSize = null)
		{
			_client = handler == null ? new HttpClient() : new HttpClient(handler, disposeHandler);

			AddDefaultHeaders(defaultRequestHeaders);
			AddRequestTimeout(timeout);
			AddMaxResponseBufferSize(maxResponseContentBufferSize);

			_endpoints = new HashSet<Uri>();
		}

		/// <summary>
		/// Gets the headers which should be sent with each request.
		/// </summary>
		public IDictionary<string, string> DefaultRequestHeaders
			=> _client.DefaultRequestHeaders.ToDictionary(x => x.Key, x => x.Value.First());

		/// <summary>
		/// Gets the time to wait before the request times out.
		/// </summary>
		public TimeSpan Timeout => _client.Timeout;

		/// <summary>
		/// Gets the maximum number of bytes to buffer when reading the response content.
		/// </summary>
		public uint MaxResponseContentBufferSize => (uint) _client.MaxResponseContentBufferSize;

		/// <summary>
		/// Gets all of the endpoints which this instance has sent a request to.
		/// </summary>
		public Uri[] Endpoints
		{
			get
			{
				lock (_endpoints)
				{
					return _endpoints.ToArray();
				}
			}
		}

		/// <summary>
		/// Sends an HTTP request as an asynchronous operation.
		/// </summary>
		public Task<HttpResponseMessage> SendAsync(HttpRequestMessage message)
		{
			AddConnectionLeaseTimeout(message.RequestUri);
			return _client.SendAsync(message);
		}

		/// <summary>
		/// Sends an HTTP request as an asynchronous operation.
		/// </summary>
		public Task<HttpResponseMessage> SendAsync(HttpRequestMessage message, CancellationToken cToken)
		{
			AddConnectionLeaseTimeout(message.RequestUri);
			return _client.SendAsync(message, cToken);
		}

		/// <summary>
		/// Sends an HTTP request as an asynchronous operation.
		/// </summary>
		public Task<HttpResponseMessage> SendAsync(HttpRequestMessage message, HttpCompletionOption option)
		{
			AddConnectionLeaseTimeout(message.RequestUri);
			return _client.SendAsync(message, option);
		}

		/// <summary>
		/// Sends an HTTP request as an asynchronous operation.
		/// </summary>
		public Task<HttpResponseMessage> SendAsync(HttpRequestMessage message, HttpCompletionOption option,
			CancellationToken cToken)
		{
			AddConnectionLeaseTimeout(message.RequestUri);
			return _client.SendAsync(message, option, cToken);
		}

		/// <summary>
		/// Clears all of the endpoints which this instance has sent a request to.
		/// </summary>
		public void ClearEndpoints()
		{
			lock (_endpoints)
			{
				_endpoints.Clear();
			}
		}

		/// <summary>
		/// Cancels all pending requests on this instance.
		/// </summary>
		public void CancelPendingRequests()
		{
			_client.CancelPendingRequests();
		}

		/// <summary>
		/// Releases the unmanaged resources and disposes of the managed resources used by the <see cref="HttpClient"/>.
		/// </summary>
		public void Dispose()
		{
			_client.Dispose();
			lock (_endpoints)
			{
				_endpoints.Clear();
			}
		}

		private void AddDefaultHeaders(IDictionary<string, string> headers)
		{
			if (headers == null)
			{
				return;
			}

			foreach (var item in headers)
			{
				_client.DefaultRequestHeaders.Add(item.Key, item.Value);
			}
		}

		private void AddRequestTimeout(TimeSpan? timeout)
		{
			if (!timeout.HasValue)
			{
				return;
			}
			_client.Timeout = timeout.Value;
		}

		private void AddMaxResponseBufferSize(ulong? size)
		{
			if (!size.HasValue)
			{
				return;
			}
			_client.MaxResponseContentBufferSize = (long) size.Value;
		}

		private void AddConnectionLeaseTimeout(Uri endpoint)
		{
			lock (_endpoints)
			{
				if (_endpoints.Contains(endpoint))
				{
					return;
				}

				//Will return in .Net Core 1.2
					//ServicePointManager.FindServicePoint(endpoint)
					//	.ConnectionLeaseTimeout = (int) 1.Minutes().TotalMilliseconds;

				_endpoints.Add(endpoint);
			}
		}
	}
}
