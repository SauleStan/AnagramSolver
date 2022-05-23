import { Component, ComponentRef, OnInit } from "@angular/core";

@Component({
    selector: 'app-body',
    templateUrl: './body.component.html',
    styleUrls: ['./body.component.css']
})
export class BodyComponent implements OnInit{
   
    constructor() {}

    ngOnInit(): void {
    }

    toggleAnagramSearchTask(){
        console.log("anagram search button clicked");
    }
}