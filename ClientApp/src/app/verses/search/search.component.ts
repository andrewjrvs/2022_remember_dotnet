import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { fromEvent, Observable } from 'rxjs';
import { debounceTime, map, switchMap, tap } from 'rxjs/operators';
import { SearchService } from '../search.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit, AfterViewInit {

  @ViewChild('search')
  private inputSearch!:ElementRef;

  public results$!: Observable<unknown>

  constructor(private searchSrv: SearchService) {

  }
  ngAfterViewInit(): void {
    this.results$ = fromEvent<KeyboardEvent>(
      this.inputSearch.nativeElement, 'keyup').pipe(
        debounceTime(1000)
        , map(e => (e?.target as HTMLInputElement).value)
        , switchMap(txt => this.searchSrv.search(txt))
      )
  }

  ngOnInit(): void {
  }

}
