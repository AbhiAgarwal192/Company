import { Component, OnInit } from '@angular/core';
import { TriangleapiService } from '../triangleapi.service';

@Component({
  selector: 'app-triangle',
  templateUrl: './triangle.component.html',
  styleUrls: ['./triangle.component.css']
})
export class TriangleComponent implements OnInit {

  triangleName: string = "";
  coordinates: string = "";
  
  x1: number = 0;
  x2: number = 0;
  x3: number = 0;

  y1: number = 0;
  y2: number = 0;
  y3: number = 0;
  outputTriangleName : string = "";

  constructor(private triangleapiservice : TriangleapiService) { 
  }

  ngOnInit(): void {
  }

  getTriangleCoordinates(){
    if(this.triangleName && this.triangleName.trim()){
      this.triangleapiservice.getTriangleCoordinates(this.triangleName).subscribe( data => {
        if(data.length == 3){
          this.coordinates = `[{${data[0][0]},${data[0][1]}},{${data[1][0]},${data[1][1]}},{${data[2][0]},${data[2][1]}}]`;
        }
      },
      error => {
        this.coordinates = error.error;
      });
    }    
  }

  getTriangleName(){
    if(this.x1>=0 && this.y1>=0 && this.x2>=0 && this.y2>=0 && this.x3>=0 && this.y3>=0){
      let coordinates = `[{${this.x1},${this.y1}},{${this.x2},${this.y2}},{${this.x3},${this.y3}}]`;
      this.triangleapiservice.getTriangleName(coordinates).subscribe( data => {
        this.outputTriangleName = data;
      },
      error => {
        this.outputTriangleName = error.error;
      });
    }    
  }
}
