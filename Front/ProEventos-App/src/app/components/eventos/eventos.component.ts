import { HttpClient } from '@angular/common/http';
import { Component, OnInit, TemplateRef } from '@angular/core';

import { EventoService } from '../../services/evento.service';
import { Evento } from '../../models/Evento';

import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
})
export class EventosComponent implements OnInit {
  modalRef?: BsModalRef;

  public eventos: Evento[] = [];
  public eventosFiltrados:Evento[] = [];

  public widthImg: number = 70;
  public marginImg: number = 2;
  public imgCollapse = false;
  private listFilterered:string = '';

  public get listFilter(){
    return this.listFilterered;
  }

  public set listFilter(value:string){
    this.listFilterered = value;
    this.eventosFiltrados = this.listFilter ? this.filtrarEventos(this.listFilter):this.eventos;
  }

  constructor(private eventoService:EventoService,
              private modalService: BsModalService,
              private toastr: ToastrService,
              private spinner: NgxSpinnerService) {}

  public ngOnInit(): void {
    this.spinner.show();
    this.getEventos();
    setTimeout(() => {
      /** spinner ends after 5 seconds */
    }, 5000);
  }

  public getEventos():void{
    this.eventoService.getEventos().subscribe(
      (eventosResp:Evento[]) => {
        this.eventos = eventosResp;
        this.eventosFiltrados = this.eventos;
      },
      (error:any) => {
        this.spinner.hide();
        this.toastr.error('Erro ao Carregar os Eventos', 'Error!')
      },
      () => this.spinner.hide()
    );
  }

  public filtrarEventos(filterBy: string): any {
    filterBy = filterBy.toLocaleLowerCase();
    return this.eventos.filter(
      (evento:any) => evento.tema.toLocaleLowerCase().indexOf(filterBy) !== -1 ||
      evento.local.toLocaleLowerCase().indexOf(filterBy) !== -1
    );
  }

  openModal(template: TemplateRef<any>): void {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef?.hide();
    this.toastr.success("O evento foi deletado com sucesso!","Deletado")
  }

  decline(): void {
    this.modalRef?.hide();
  }
}
