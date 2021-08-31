import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { PagingHeaderComponent } from './components/paging-header/paging-header.component';
import { PagerComponent } from './components/pager/pager.component';


@NgModule({
  declarations: [
    PagingHeaderComponent,
    PagerComponent
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot(),           // for root bc paginationModule has its own provider's array and those providers need ot be injected to root module at start up
    CarouselModule.forRoot()
  ],

  exports: [
    PaginationModule,
    PagingHeaderComponent,
    PagerComponent,
    CarouselModule
  ]
})
export class SharedModule { }
