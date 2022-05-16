import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
//import { EventEmitter } from 'stream';

@Component({
  selector: 'app-silly-button',
  templateUrl: './silly-button.component.html',
  styleUrls: ['./silly-button.component.css'],
})
export class SillyButtonComponent implements OnInit {
  @Input() text!: string;
  @Input() color!: string;
  @Output() btnClick = new EventEmitter();

  constructor() {}

  ngOnInit(): void {}

  onClick() {
    //this.btnClick.emit();
    window.open('https://www.youtube.com/watch?v=I4Q3YDezqcM', '_blank');
  }
}
