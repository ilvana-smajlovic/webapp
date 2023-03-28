import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Updatepassword1Component } from './updatepassword1.component';

describe('Updatepassword1Component', () => {
  let component: Updatepassword1Component;
  let fixture: ComponentFixture<Updatepassword1Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ Updatepassword1Component ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Updatepassword1Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
