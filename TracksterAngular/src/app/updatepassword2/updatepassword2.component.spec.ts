import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Updatepassword2Component } from './updatepassword2.component';

describe('Updatepassword2Component', () => {
  let component: Updatepassword2Component;
  let fixture: ComponentFixture<Updatepassword2Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ Updatepassword2Component ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Updatepassword2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
