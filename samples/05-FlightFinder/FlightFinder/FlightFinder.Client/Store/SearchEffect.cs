﻿using Blazor.Fluxor;
using FlightFinder.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;

namespace FlightFinder.Client.Store
{
	public class SearchEffect : Effect<SearchAction>
	{
		private readonly HttpClient HttpClient;

		public SearchEffect(HttpClient httpClient)
		{
			HttpClient = httpClient;
		}

		protected async override Task HandleAsync(SearchAction action, IDispatcher dispatcher)
		{
			try
			{
				Itinerary[] searchResults = await HttpClient.PostJsonAsync<Itinerary[]>("/api/flightsearch", action.SearchCriteria);
				dispatcher.Dispatch(new SearchCompleteAction(searchResults));
			}
			catch
			{
				// Should really dispatch an error action
				dispatcher.Dispatch(new SearchCompleteAction(null));
			}
		}
	}
}
