import { Component, Input, OnInit } from "@angular/core";
import { Word } from "../../Word";

@Component({
    selector: 'app-word-item',
    templateUrl: './word-item.component.html',
    styleUrls: ['./word-item.component.css']
})
export class WordItemComponent implements OnInit{
    @Input()
    word!: Word;

    constructor() {}

    ngOnInit(): void {
    }
    
}