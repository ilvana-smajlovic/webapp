import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MedialistComponent } from './medialist.component';

describe('MedialistComponent', () => {
  let component: MedialistComponent;
  let fixture: ComponentFixture<MedialistComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MedialistComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MedialistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
