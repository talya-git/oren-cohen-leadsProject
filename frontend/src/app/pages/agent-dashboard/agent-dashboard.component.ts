import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { LeadsService, Lead } from '../../services/leads.service';

@Component({
  selector: 'app-agent-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './agent-dashboard.component.html',
  styleUrl: './agent-dashboard.component.scss'
})
export class AgentDashboardComponent implements OnInit {
  leads: Lead[] = [];
  selectedLead: Lead | null = null;
  showCallForm = false;
  newCallTitle = '';
  newCallSummary = '';

  propertyTypes = ['דירה', 'פנטהאוז', 'דירת גן', 'בית פרטי', 'וילה', 'דופלקס', 'סטודיו'];
  allAmenities = ['מרפסת', 'מחסן', 'חניה', 'ממד', 'מעלית', 'גישה לנכים', 'נוף'];
  allNearBy = ['בית כנסת', 'סופרים'];

  constructor(public auth: AuthService, private leadsService: LeadsService) {}

  ngOnInit(): void {
    this.loadLeads();
  }

  loadLeads(): void {
    this.leadsService.getMyLeads().subscribe(leads => this.leads = leads);
  }

  openDetail(lead: Lead): void { this.selectedLead = lead; }
  closeDetail(): void { this.selectedLead = null; this.showCallForm = false; }

  getTranscript(lead: Lead): any[] {
    try { return JSON.parse(lead.transcript || '[]'); } catch { return []; }
  }

  getPhoneCalls(lead: Lead): any[] {
    try { return JSON.parse((lead as any).phoneCalls || '[]'); } catch { return []; }
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

  updateField(field: string, event: Event): void {
    const value = (event.target as HTMLInputElement).value;
    this.updateProperty(field, value);
  }

  toggleCallForm(): void {
    this.showCallForm = !this.showCallForm;
    this.newCallTitle = '';
    this.newCallSummary = '';
  }

  addPhoneCall(): void {
    if (!this.selectedLead || !this.newCallSummary.trim()) return;
    const agent = this.auth.currentUser?.name || '';
    this.leadsService.addPhoneCall(this.selectedLead.id, agent, this.newCallTitle, this.newCallSummary).subscribe(() => {
      this.showCallForm = false;
      this.newCallTitle = '';
      this.newCallSummary = '';
      this.leadsService.getMyLeads().subscribe(leads => {
        this.leads = leads;
        this.selectedLead = leads.find(l => l.id === this.selectedLead!.id) || null;
      });
    });
  }

  logout(): void { this.auth.logout(); }
}
