import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TvseriesWatchlistComponent } from './tvseries-watchlist.component';

describe('TvseriesWatchlistComponent', () => {
  let component: TvseriesWatchlistComponent;
  let fixture: ComponentFixture<TvseriesWatchlistComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TvseriesWatchlistComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TvseriesWatchlistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
