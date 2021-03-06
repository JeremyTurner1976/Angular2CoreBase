import { Component } from "@angular/core";

@Component({
	selector: "counter",
	template: require("./counter.component.html")
})
export class CounterComponent {
	currentCount = 0;

	incrementCounter() {
		this.currentCount++;
	}
}