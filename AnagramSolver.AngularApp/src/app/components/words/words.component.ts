import { Component, OnInit } from "@angular/core";
import { Word } from "../../Word";
import { WordService } from "../../services/word.service";

@Component({
    selector: "app-words",
    templateUrl: './words.component.html',
    styleUrls: ['./words.component.css']
})
export class WordsComponent implements OnInit{
    words: Word[] = [];

    constructor(private wordService: WordService){}

    ngOnInit(): void {
        this.wordService.getWords().subscribe((fetchedWords) =>{
            this.words = fetchedWords;
        });
        
    }

}