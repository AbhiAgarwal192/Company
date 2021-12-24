import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})

export class TriangleapiService {

  constructor(private http : HttpClient) { }

  getTriangleCoordinates(triangleName : string ) : Observable<any>{
    return this.http.get<any>(environment.triangleAPIBaseUrl + 'api/triangle/' + triangleName + '/coordinates');
  }

  getTriangleName(coordinates : string) : Observable<string> {
    return this.http.get(environment.triangleAPIBaseUrl + 'api/triangle/name?coordinates=' + coordinates,{responseType: 'text'});
  }
}
