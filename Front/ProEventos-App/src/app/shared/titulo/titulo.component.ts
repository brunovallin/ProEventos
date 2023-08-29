import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-titulo',
  templateUrl: './titulo.component.html',
  styleUrls: ['./titulo.component.scss']
})
export class TituloComponent implements OnInit {
  @Input() subtitulo: string = 'Desde 2023';
  @Input() iconClass: string = 'fa fa-user';
  @Input() titulo: string = '';
  @Input() botaoListar:boolean = false;
  constructor(private router:Router) { }

  ngOnInit() {
  }

  listar():void{
    this.router.navigate([`/${this.titulo.toLocaleLowerCase()}/lista`])
  }

}
