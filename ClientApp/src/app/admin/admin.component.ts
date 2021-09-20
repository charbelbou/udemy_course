import { Component, OnInit } from "@angular/core";

@Component({
  templateUrl: "admin.component.html",
})
export class AdminComponent implements OnInit {
  // Pie chart data
  // types of cars and quantities
  data = {
    labels: ["BMW", "Audi", "Mazda"],
    datasets: [
      {
        data: [5, 3, 1],
        backgroundColor: ["#ff6384", "#36a2eb", "#afce26"],
      },
    ],
  };
  constructor() {}

  ngOnInit() {}
}
