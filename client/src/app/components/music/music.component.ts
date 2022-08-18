import { Component, OnInit } from '@angular/core';
import { Music } from 'src/app/Music';
import { MockMusic } from 'src/app/MockMusic';

@Component({
  selector: 'app-music',
  templateUrl: './music.component.html',
  styleUrls: ['./music.component.scss']
})
export class MusicComponent implements OnInit {

  musicItems: Music[] = MockMusic;
  
  constructor() { }

  ngOnInit(): void {
  }

}
