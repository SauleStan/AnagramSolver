import { Injectable } from "@angular/core";
import {HttpClient, HttpHeaders} from '@angular/common/http'
import { Observable, of } from "rxjs";
import { WORDS } from "../mock-words";
import { Word } from "../Word";

@Injectable({
    providedIn: 'root'
})
export class WordService{
    private apiUrl = 'https://localhost:7188/api/Words'
    
    constructor(private httpClient:HttpClient) {}

    getWords(): Observable<Word[]> {
        return this.httpClient.get<Word[]>(this.apiUrl);
    }
}