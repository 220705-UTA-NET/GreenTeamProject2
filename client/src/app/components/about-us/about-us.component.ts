import { Component, Input, OnInit } from '@angular/core';

interface aboutUsImage {
  docSrc: string;
  docAlt: string;
}

@Component({
  selector: 'app-about-us',
  templateUrl: './about-us.component.html',
  styleUrls: ['./about-us.component.css']
})

export class AboutUsComponent implements OnInit {
  
  @Input() documents: aboutUsImage[] = []
  selectedIndex = 0;

  ngOnInit(): void {
  }
}
