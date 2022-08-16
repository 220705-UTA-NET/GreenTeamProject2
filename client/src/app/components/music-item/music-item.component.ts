import { Component, OnInit, Input} from '@angular/core';
import { Music } from 'src/app/Music';

@Component({
  selector: 'app-music-item',
  templateUrl: './music-item.component.html',
  styleUrls: ['./music-item.component.scss']
})
export class MusicItemComponent implements OnInit {

  @Input() item: Music 
  
  constructor() { }

  ngOnInit(): void {
  }

}
