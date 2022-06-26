import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SearchComponent } from './search/search.component';
import { VersesComponent } from './verses.component';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { SearchService } from './search.service';



@NgModule({
  declarations: [
    SearchComponent,
    VersesComponent
  ],
  imports: [
    HttpClientModule,
    CommonModule,
    RouterModule.forChild([
      { path: '', component: VersesComponent, children: [
        { path: '', redirectTo: 'search' }
        , { path: 'search', component: SearchComponent }
      ] },
    ])
  ],
  providers: [SearchService],
  exports: [RouterModule]
})
export class VersesModule { }
