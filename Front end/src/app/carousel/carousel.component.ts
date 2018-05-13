import { Component, OnInit } from '@angular/core';
import { Image } from '../models/image';

@Component({
  selector: 'app-carousel',
  templateUrl: './carousel.component.html',
  styleUrls: ['./carousel.component.css']
})
export class CarouselComponent implements OnInit {

  images: Image[];

  constructor() { 
    this.images = [
      new Image("Sesto Elemento", "./assets/Images/Sesto-Elemento.jpg"),
      new Image("La Ferrari", "./assets/Images/ferrari-laferrari.png"),
      new Image("Koinegsegg Agera R","./assets/Images/agerar.jpg")
    ];
  }

  ngOnInit() {
  }

}
