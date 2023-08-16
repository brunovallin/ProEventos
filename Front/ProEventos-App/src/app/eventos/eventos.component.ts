import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
})
export class EventosComponent implements OnInit {
  public eventos: any = [];
  public eventosFiltrados:any = [];
  widthImg:number = 70;
  marginImg:number = 2;
  imgCollapse = true;
  private _listFilter:string = '';

  public get listFilter(){
    return this._listFilter;
  }

  public set listFilter(value:string){
    this._listFilter = value;
    this.eventosFiltrados = this.listFilter ? this.filtrarEventos(this.listFilter):this.eventos;
  }

  constructor(private http:HttpClient) {}

  ngOnInit(): void {
    this.getEventos();
  }

  public getEventos():void{
    this.http.get('https://localhost:5001/api/eventos').subscribe(
      response => {
        this.eventos = response
        this.eventosFiltrados = this.eventos
      },
      error => console.log(error)
    );

  }

  filtrarEventos(filterBy: string): any {
    filterBy = filterBy.toLocaleLowerCase();
    return this.eventos.filter(
      (evento:any) => evento.tema.toLocaleLowerCase().indexOf(filterBy) !== -1 ||
      evento.local.toLocaleLowerCase().indexOf(filterBy) !== -1
    );
  }
}
