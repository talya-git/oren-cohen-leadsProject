import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { LeadsService, Lead, Agent } from '../../services/leads.service';

@Component({
  selector: 'app-agent-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './agent-dashboard.component.html',
  styleUrl: './agent-dashboard.component.scss'
})
export class AgentDashboardComponent implements OnInit {
  myLeads: Lead[] = [];
  allLeads: Lead[] = [];
  filteredLeads: Lead[] = [];
  agents: Agent[] = [];
  selectedLead: Lead | null = null;
  showCallForm = false;
  newCallTitle = '';
  newCallSummary = '';
  newAmenity = '';
  newNearBy = '';
  activeTab: 'my' | 'all' = 'my';
  currentPage = 1;
  pageSize = 50;

  // Filters
  filterAgent = '';
  filterArea = '';
  filterProject = '';
  filterFreeText = '';

  propertyTypes = ['דירה', 'פנטהאוז', 'דירת גן', 'בית פרטי', 'וילה', 'דופלקס', 'סטודיו'];
  allAmenities: string[] = [];
  allNearBy: string[] = [];
  allObjections = [
    'כספי', 'לא בתקציב', 'מחיר', 'תנאי תשלום', 'מדד תשומות הבניה',
    'מיקום לא מתאים', 'כביש ראשי',
    'מרפסת/גינה', 'תכנון הדירה', 'שטח הדירה', 'כיווני אוויר', 'חוסר חניה', 'חניה לא מתאימה',
    'חוסר מחסן', 'חוסר מעלית שבת',
    'אין דירה מתאימה', 'לא מעוניין בתמ״א 38',
    'היתר בנייה', 'מלאי הדירות אזל', 'קבוצת רכישה', 'ממתין לתחילת ביצוע', 'בניין רב קומות',
    'קומה',
    'קנה ידԲ2', 'רכשו בפרויקט אחר', 'בעיות מימון מצד הלקוח',
    'קנה בעיר אחרת', 'מחפשים בעיר אחרת', 'ירדו מקנייה',
    'מוכרים דירה קיימת', 'ממתינים לבניין הבא', 'השכרה',
    'הסר',
    'מו״מ נכשל', 'מו״מ משפטי נכשל', 'ביטול עסקה אחרי הסכם',
    'ביטול הרשמה', 'אין מענה', 'פניה לא רלוונטית לשיווק', 'ריגול תעשייתי', 'ליד פסול!',
    'בטחונות לא מספקות',
    'יצירת קשר באחריות הלקוח', 'התנגדות לא מוגדרת',
    'קורונה', 'התיישנות', 'מאגר ישן'
  ];

  constructor(public auth: AuthService, private leadsService: LeadsService) {}

  ngOnInit(): void {
    this.loadLeads();
    this.leadsService.getAgents().subscribe(a => this.agents = a);
    this.leadsService.getAmenityOptions().subscribe(a => this.allAmenities = a.map(x => x.name));
    this.leadsService.getNearByOptions().subscribe(n => this.allNearBy = n.map(x => x.name));
  }

  loadLeads(): void {
    this.leadsService.getMyLeads().subscribe(leads => this.myLeads = leads);
    this.leadsService.getAllLeadsForAgent().subscribe(leads => {
      this.allLeads = leads;
      this.applyFilters();
    });
  }

  switchTab(tab: 'my' | 'all'): void {
    this.activeTab = tab;
    this.currentPage = 1;
    this.applyFilters();
  }

  applyFilters(): void {
    let leads = this.activeTab === 'my' ? this.myLeads : this.allLeads;

    if (this.filterAgent) {
      leads = leads.filter(l => l.assignedTo?.name === this.filterAgent);
    }
    if (this.filterArea) {
      leads = leads.filter(l => l.area?.toLowerCase().includes(this.filterArea.toLowerCase()));
    }
    if (this.filterProject) {
      leads = leads.filter(l => l.referralProject?.toLowerCase().includes(this.filterProject.toLowerCase()));
    }
    if (this.filterFreeText) {
      const q = this.filterFreeText.toLowerCase();
      leads = leads.filter(l =>
        (l.contactName || '').toLowerCase().includes(q) ||
        (l.phone || '').includes(q) ||
        (l.area || '').toLowerCase().includes(q) ||
        (l.notes || '').toLowerCase().includes(q) ||
        (l.budget || '').toLowerCase().includes(q) ||
        (l.source || '').toLowerCase().includes(q) ||
        (l.referralProject || '').toLowerCase().includes(q)
      );
    }

    this.filteredLeads = leads;
    this.currentPage = 1;
  }

  get totalPages(): number {
    return Math.ceil(this.filteredLeads.length / this.pageSize) || 1;
  }

  get paginatedLeads(): Lead[] {
    const start = (this.currentPage - 1) * this.pageSize;
    return this.filteredLeads.slice(start, start + this.pageSize);
  }

  get visiblePages(): number[] {
    const pages: number[] = [];
    const start = Math.max(1, this.currentPage - 2);
    const end = Math.min(this.totalPages, this.currentPage + 2);
    for (let i = start; i <= end; i++) pages.push(i);
    return pages;
  }

  goToPage(page: number): void {
    if (page >= 1 && page <= this.totalPages) this.currentPage = page;
  }

  get uniqueAreas(): string[] {
    const areas = this.allLeads.map(l => l.area).filter(a => !!a) as string[];
    return [...new Set(areas)];
  }

  get uniqueProjects(): string[] {
    const projects = this.allLeads.map(l => l.referralProject).filter(p => !!p) as string[];
    return [...new Set(projects)];
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

  getObjections(lead: Lead): string[] {
    try { return JSON.parse((lead as any).objections || '[]'); } catch { return []; }
  }

  isObjectionSelected(obj: string): boolean {
    return this.getObjections(this.selectedLead!).includes(obj);
  }

  toggleObjection(obj: string): void {
    const current = this.getObjections(this.selectedLead!);
    const updated = current.includes(obj) ? current.filter(o => o !== obj) : [...current, obj];
    this.updateProperty('objections', JSON.stringify(updated));
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

  addAmenity(): void {
    if (!this.newAmenity.trim()) return;
    this.leadsService.addAmenityOption(this.newAmenity.trim()).subscribe(() => {
      this.leadsService.getAmenityOptions().subscribe(a => this.allAmenities = a.map(x => x.name));
      this.toggleAmenity(this.newAmenity.trim());
      this.newAmenity = '';
    });
  }

  addNearBy(): void {
    if (!this.newNearBy.trim()) return;
    this.leadsService.addNearByOption(this.newNearBy.trim()).subscribe(() => {
      this.leadsService.getNearByOptions().subscribe(n => this.allNearBy = n.map(x => x.name));
      this.toggleNearBy(this.newNearBy.trim());
      this.newNearBy = '';
    });
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
      this.loadLeads();
      this.leadsService.getAllLeadsForAgent().subscribe(leads => {
        this.selectedLead = leads.find(l => l.id === this.selectedLead!.id) || null;
      });
    });
  }

  logout(): void { this.auth.logout(); }
}
