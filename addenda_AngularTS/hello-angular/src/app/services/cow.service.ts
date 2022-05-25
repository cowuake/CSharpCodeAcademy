import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { Observable } from 'rxjs';
import { ICow } from 'src/app/Cow';
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

  getCows(): Observable<ICow[]> {
    //const cows = of(COWS);
    //return cows;
    return this.http.get<ICow[]>(this.apiUrl);
  }

  deleteCow(cow: ICow): Observable<ICow> {
    const url = `${this.apiUrl}/${cow.id}`;
    return this.http.delete<ICow>(url);
  }

  updateCowFavorite(cow: ICow): Observable<ICow> {
    const url = `${this.apiUrl}/${cow.id}`;
    return this.http.put<ICow>(url, cow, httpOptions);
  }

  addCow(cow: ICow): Observable<ICow> {
    return this.http.post<ICow>(this.apiUrl, cow, httpOptions);
  }
}
