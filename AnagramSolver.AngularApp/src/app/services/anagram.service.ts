import { Injectable } from "@angular/core";
import {HttpClient} from '@angular/common/http'
import { Word } from "../Word";

@Injectable({
    providedIn: 'root'
})
export class AnagramService{
    private apiUrl = 'https://localhost:7188/api/Anagram'

    constructor(private httpClient:HttpClient) {}

    getAnagrams(input: string){
        var url = this.apiUrl + `/${input}`
        return this.httpClient.get<Word[]>(url);
    }
}