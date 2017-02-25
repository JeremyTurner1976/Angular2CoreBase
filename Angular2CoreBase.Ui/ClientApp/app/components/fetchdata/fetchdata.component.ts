import { Component } from "@angular/core";
import { Http } from "@angular/http";

@Component({
	selector: "fetchdata",
	template: require("./fetchdata.component.html")
})
export class FetchDataComponent {
	public data: weatherData;

	constructor(http: Http) {
		this.data = {
			description: "",
			sunrise: "",
			sunset: "",
			city: "",
			country: "",
			weatherForecasts: []
		}

		http.get("/api/WeatherData/WeatherForecasts").subscribe(result => {
			console.log(result);
			try {
				this.data = result.json();
				console.log(this.data);
				console.log(this.data.weatherForecasts);
			}catch(error) {
				console.log(error);
			}
		});
	}
}

export interface weatherData {
	description: string;
	sunrise: string;
	sunset: string;
	city: string;
	country: string;
	weatherForecasts: WeatherForecast[];
}

export interface WeatherForecast {
	startDateTime: string;
	endDateTime: string;
	description: string;
	temperature: number;
	minimumTemperature: number;
	maximumTemperature: number;
	humidity: number;
	atmosphericPressure: number;
	windspeed: number;
	windDirection: number;
	skyCon: string;
	cloudCover: number;
	rainVolume: number;
	anowVolume: number;
}
