import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { Observable } from 'rxjs';
import { Cow } from 'src/app/Cow';
//import { COWS } from 'src/app/mock-cows';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class CowService {
  private apiUrl = 'http://localhost:5000/cows';

  constructor(private http: HttpClient) { }

  getCows(): Observable<Cow[]> {
    //const cows = of(COWS);
    //return cows;
    return this.http.get<Cow[]>(this.apiUrl);
  }

  deleteCow(cow: Cow): Observable<Cow> {
    const url = `${this.apiUrl}/${cow.id}`;
    return this.http.delete<Cow>(url);
  }

  updateCowFavorite(cow: Cow): Observable<Cow> {
    const url = `${this.apiUrl}/${cow.id}`;
    return this.http.put<Cow>(url, cow, httpOptions);
  }

  addCow(cow: Cow): Observable<Cow> {
    return this.http.post<Cow>(this.apiUrl, cow, httpOptions);
  }
}
