import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

export class Message {
  content: string;
  author: string;
}

@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.css']
})
export class TestComponent implements OnInit {

  backendResponse: string;
  firstName: string;
  lastName: string;
  message: string;
  messages: Array<Message>; 

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.http.get("https://localhost:44378/" + "account" + "/getCurrentUser").subscribe(response => {
      this.firstName = (response as any).firstName;
      this.lastName = (response as any).lastName;
      this.refreshMessages();
    },
      error => {
        //this.backendResponse = error;
      });  
  }

  refreshMessages() {
    this.http.get<Array<Message>>("https://localhost:44378/" + "kurs" + "/sendMessage").subscribe(response => {
      this.messages = response; 
    },
      error => {
        this.backendResponse = error;
      });

  }

  sendRequestToBackend() {
    var message = new Message();
    message.content = this.message;
    message.author = this.firstName + " " + this.lastName;

    this.http.post<Message>("https://localhost:44378/" + "kurs" + "/sendMessage", message).subscribe(response => {
      this.backendResponse = response.content;
    },
      error => {
        this.backendResponse = error;
      });
  }
}
