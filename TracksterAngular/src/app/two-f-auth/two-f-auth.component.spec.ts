import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TwoFAuthComponent } from './two-f-auth.component';

describe('TwoFAuthComponent', () => {
  let component: TwoFAuthComponent;
  let fixture: ComponentFixture<TwoFAuthComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TwoFAuthComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TwoFAuthComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
