import "angular2-universal-polyfills";
import "zone.js";
import { enableProdMode } from "@angular/core";
import { platformNodeDynamic } from "angular2-universal";
import { AppModule } from "./app/app.module";

enableProdMode();
const platform: any = platformNodeDynamic();

export default function(params: any): Promise<{ html: string, globals?: any }> {
	return new Promise((resolve: any, reject: any) => {
		const requestZone = Zone.current.fork({
			name: "angular-universal request",
			properties: {
				baseUrl: "/",
				requestUrl: params.url,
				originUrl: params.origin,
				preboot: false,
				// TODO: render just the <app> component instead of wrapping it inside an extra HTML document
				// waiting on https://github.com/angular/universal/issues/347
				document: "<!DOCTYPE html><html><head></head><body><app></app></body></html>"
			},
			onHandleError: (parentZone, currentZone, targetZone, error) => {
				// if any error occurs while rendering the module, reject the whole operation
				reject(error);
				return true;
			}
		});

		return requestZone.run<Promise<string>>(() => platform.serializeModule(AppModule)).then(html => {
			resolve({ html: html });
		}, reject);
	});
}