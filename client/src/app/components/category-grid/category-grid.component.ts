import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-category-grid',
  templateUrl: './category-grid.component.html',
  styleUrls: ['./category-grid.component.scss']
})
export class CategoryGridComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }


  goToDocumentPage() {
    console.log("It works!");
  }
  goToMusicPage() {
    console.log("It works!");
  }
  goToAudiobookPage() {
    console.log("It works!");
  }
  goToNFTPage() {
    console.log("It works!");
  }
  goToAllPage() {
    console.log("It works!");
  }
}
