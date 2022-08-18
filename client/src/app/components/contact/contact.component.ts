import { Component, OnInit } from '@angular/core';
import { GlobalService } from 'src/app/shared/globalUser/globalUser.service';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})
export class ContactComponent implements OnInit {

  constructor(private user: GlobalService) { }

  ngOnInit(): void {
    this.user.cust_id = 10;
  }

}
