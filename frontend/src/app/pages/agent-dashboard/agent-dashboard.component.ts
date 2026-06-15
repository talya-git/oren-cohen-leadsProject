import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import { LeadsService, Lead } from '../../services/leads.service';

@Component({
  selector: 'app-agent-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './agent-dashboard.component.html',
  styleUrl: './agent-dashboard.component.scss'
})
export class AgentDashboardComponent implements OnInit {
  leads: Lead[] = [];
  selectedLead: Lead | null = null;

  constructor(public auth: AuthService, private leadsService: LeadsService) {}

  ngOnInit(): void {
    this.leadsService.getMyLeads().subscribe(leads => this.leads = leads);
  }

  openDetail(lead: Lead): void { this.selectedLead = lead; }
  closeDetail(): void { this.selectedLead = null; }

  getTranscript(lead: Lead): any[] {
    try { return JSON.parse(lead.transcript || '[]'); } catch { return []; }
  }

  logout(): void { this.auth.logout(); }
}
