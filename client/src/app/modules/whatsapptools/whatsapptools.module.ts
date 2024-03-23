import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';


import { SharedModule } from 'src/app/shared/shared.module';
import { MaterialModule } from 'src/app/shared/material/material.module';
import { WhatsapptoolsAccountRoutingModule } from './whatsapptools-routing-module';
import { StatisticsComponent } from './statistics/statistics.component';


@NgModule({
  declarations: [
    StatisticsComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    SharedModule,
    WhatsapptoolsAccountRoutingModule,
  ]
})
export class WhatsapptoolsModule { }
