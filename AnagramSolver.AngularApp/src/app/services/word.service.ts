import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
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