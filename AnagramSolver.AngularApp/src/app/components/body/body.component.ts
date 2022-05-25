import { Component, OnInit } from "@angular/core";
import { FormBuilder } from '@angular/forms';
import { AnagramService } from "src/app/services/anagram.service";
import { Word } from "src/app/Word";

@Component({
    selector: 'app-body',
    templateUrl: './body.component.html',
    styleUrls: ['./body.component.css']
})
export class BodyComponent implements OnInit{
    searchForm = this.formBuilder.group({
        inputWord: ''
    });

    anagrams: Word[] = [];
   
    constructor(private formBuilder: FormBuilder,
        private anagramService: AnagramService) {}

    ngOnInit(): void {
    }

    onSubmit(): void {
        this.anagramService.getAnagrams(this.searchForm.value['inputWord'])
            .subscribe((fetchedAnagrams) => {
                this.anagrams = fetchedAnagrams;
            });
        this.searchForm.reset();
    }
}