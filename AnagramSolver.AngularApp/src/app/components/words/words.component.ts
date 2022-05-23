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
        debugger
        /***var fetchedWords = this.wordService.getWords().subscribe((fetchedWords) => {
            fetchedWords.forEach(fetchedWord => fetchedWord.)
        });***/

        this.wordService.getWords().subscribe((fetchedWords) =>{
            console.log(fetchedWords);
            this.words = fetchedWords;
        });
        
    }

}