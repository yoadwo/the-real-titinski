import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RantDetailComponent } from './rant-detail.component';

describe('RantDetailComponent', () => {
  let component: RantDetailComponent;
  let fixture: ComponentFixture<RantDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RantDetailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RantDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
