import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { LeadsService, Lead, Agent } from '../../services/leads.service';

@Component({
  selector: 'app-manager-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './manager-dashboard.component.html',
  styleUrl: './manager-dashboard.component.scss'
})
export class ManagerDashboardComponent implements OnInit {
  leads: Lead[] = [];
  filteredLeads: Lead[] = [];
  agents: Agent[] = [];
  currentFilter = 'all';
  selectedLead: Lead | null = null;
  showAssignModal = false;
  showDetailModal = false;

  propertyTypes = ['דירה', 'פנטהאוז', 'דירת גן', 'בית פרטי', 'וילה', 'דופלקס', 'סטודיו'];
  allAmenities = ['מרפסת', 'מחסן', 'חניה', 'ממד', 'מעלית', 'גישה לנכים', 'נוף'];
  allNearBy = ['בית כנסת', 'סופרים'];

  constructor(public auth: AuthService, private leadsService: LeadsService) {}

  ngOnInit(): void {
    this.loadLeads();
    this.leadsService.getAgents().subscribe(a => this.agents = a);
  }

  loadLeads(): void {
    this.leadsService.getAllLeads().subscribe(leads => {
      this.leads = leads;
      this.applyFilter();
    });
  }

  filter(type: string): void {
    this.currentFilter = type;
    this.applyFilter();
  }

  applyFilter(): void {
    if (this.currentFilter === 'all') this.filteredLeads = this.leads;
    else if (this.currentFilter === 'new') this.filteredLeads = this.leads.filter(l => l.status === 'new');
    else if (this.currentFilter === 'assigned') this.filteredLeads = this.leads.filter(l => l.status === 'assigned');
    else this.filteredLeads = this.leads.filter(l => l.rating === this.currentFilter);
  }

  openDetail(lead: Lead): void {
    this.selectedLead = lead;
    this.showDetailModal = true;
  }

  closeDetail(): void {
    this.showDetailModal = false;
    this.selectedLead = null;
  }

  rateLead(id: number, rating: string): void {
    this.leadsService.updateLead(id, { rating } as any).subscribe(() => {
      this.loadLeads();
      this.closeDetail();
    });
  }

  openAssign(lead: Lead): void {
    this.selectedLead = lead;
    this.showAssignModal = true;
  }

  closeAssign(): void {
    this.showAssignModal = false;
  }

  assignTo(agentId: number): void {
    if (!this.selectedLead) return;
    this.leadsService.assignLead(this.selectedLead.id, agentId).subscribe(() => {
      this.showAssignModal = false;
      this.loadLeads();
      this.closeDetail();
    });
  }

  deleteLead(id: number): void {
    if (!confirm('למחוק את הליד?')) return;
    this.leadsService.deleteLead(id).subscribe(() => {
      this.loadLeads();
      this.closeDetail();
    });
  }

  getTranscript(lead: Lead): any[] {
    try { return JSON.parse(lead.transcript || '[]'); } catch { return []; }
  }

  getAmenities(lead: Lead): string[] {
    try { return JSON.parse(lead.amenities || '[]'); } catch { return []; }
  }

  getNearBy(lead: Lead): string[] {
    try { return JSON.parse(lead.nearBy || '[]'); } catch { return []; }
  }

  isAmenitySelected(amenity: string): boolean {
    return this.getAmenities(this.selectedLead!).includes(amenity);
  }

  isNearBySelected(place: string): boolean {
    return this.getNearBy(this.selectedLead!).includes(place);
  }

  toggleAmenity(amenity: string): void {
    const current = this.getAmenities(this.selectedLead!);
    const updated = current.includes(amenity) ? current.filter(a => a !== amenity) : [...current, amenity];
    this.updateProperty('amenities', JSON.stringify(updated));
  }

  toggleNearBy(place: string): void {
    const current = this.getNearBy(this.selectedLead!);
    const updated = current.includes(place) ? current.filter(n => n !== place) : [...current, place];
    this.updateProperty('nearBy', JSON.stringify(updated));
  }

  updateProperty(field: string, value: any): void {
    if (!this.selectedLead) return;
    const data: any = {};
    data[field] = value;
    this.leadsService.updateLead(this.selectedLead.id, data).subscribe(() => {
      (this.selectedLead as any)[field] = value;
      this.loadLeads();
    });
  }

  logout(): void { this.auth.logout(); }
}
