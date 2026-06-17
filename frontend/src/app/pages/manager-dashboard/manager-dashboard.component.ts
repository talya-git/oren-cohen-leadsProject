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
  currentPage = 1;
  pageSize = 50;

  propertyTypes = ['דירה', 'פנטהאוז', 'דירת גן', 'בית פרטי', 'וילה', 'דופלקס', 'סטודיו'];
  allAmenities = ['מרפסת', 'מחסן', 'חניה', 'ממד', 'מעלית', 'גישה לנכים', 'נוף'];
  allNearBy = ['בית כנסת', 'סופרים'];
  newProjectName = '';
  projectsList = [
    'שמואל הנגיד-רזדנס', 'עדן', 'יפו 184', 'השלושה',
    'Oren Cohen יד 2', 'ניתאי הארבלי',
    'סוקולוב 6 - טלביה פארק', 'ספקים',
    'קרן היסוד 15 - בן דוד', 'מאיר שחם - בן דוד',
    'לינקולן - בן דוד', 'אינדפנדנס - מאיר שחם 3-1',
    'נוף הנגיד - שמואל הנגיד 15',
    'תיבת האוצרות', 'בית הערבה'
  ];
  allObjections = [
    // כספי
    'כספי', 'לא בתקציב', 'מחיר', 'תנאי תשלום', 'מדד תשומות הבניה',
    // מיקום
    'מיקום לא מתאים', 'כביש ראשי',
    // תכנון
    'מרפסת/גינה', 'תכנון הדירה', 'שטח הדירה', 'כיווני אוויר', 'חוסר חניה', 'חניה לא מתאימה',
    'חוסר מחסן', 'חוסר מעלית שבת',
    // פרויקט כללי
    'אין דירה מתאימה', 'לא מעוניין בתמ״א 38',
    // מועד איכלוס
    'היתר בנייה', 'מלאי הדירות אזל', 'קבוצת רכישה', 'ממתין לתחילת ביצוע', 'בניין רב קומות',
    // קומה
    'קומה',
    // סטטוס לקוח
    'קנה ידԲ2', 'רכשו בפרויקט אחר', 'בעיות מימון מצד הלקוח',
    'קנה בעיר אחרת', 'מחפשים בעיר אחרת', 'ירדו מקנייה',
    // מוכרים דירה קיימת
    'מוכרים דירה קיימת', 'ממתינים לבניין הבא', 'השכרה',
    // הסר
    'הסר',
    // תהליך רכישה
    'מו״מ נכשל', 'מו״מ משפטי נכשל', 'ביטול עסקה אחרי הסכם',
    // שלב פניה
    'ביטול הרשמה', 'אין מענה', 'פניה לא רלוונטית לשיווק', 'ריגול תעשייתי', 'ליד פסול!',
    // משפטי
    'בטחונות לא מספקות',
    // שונות
    'יצירת קשר באחריות הלקוח', 'התנגדות לא מוגדרת',
    // קורונה
    'קורונה', 'התיישנות', 'מאגר ישן'
  ];

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

  // Phone calls
  showCallForm = false;
  newCallTitle = '';
  newCallSummary = '';

  getPhoneCalls(lead: Lead): any[] {
    try { return JSON.parse((lead as any).phoneCalls || '[]'); } catch { return []; }
  }

  toggleCallForm(): void {
    this.showCallForm = !this.showCallForm;
    this.newCallTitle = '';
    this.newCallSummary = '';
  }

  addPhoneCall(): void {
    if (!this.selectedLead || !this.newCallSummary.trim()) return;
    const agent = this.auth.currentUser?.name || 'מנהל';
    this.leadsService.addPhoneCall(this.selectedLead.id, agent, this.newCallTitle, this.newCallSummary).subscribe(() => {
      this.showCallForm = false;
      this.newCallSummary = '';
      this.loadLeads();
      // refresh selected lead
      this.leadsService.getAllLeads().subscribe(leads => {
        this.selectedLead = leads.find(l => l.id === this.selectedLead!.id) || null;
      });
    });
  }

  addProject(): void {
    if (!this.newProjectName.trim()) return;
    if (!this.projectsList.includes(this.newProjectName.trim())) {
      this.projectsList.push(this.newProjectName.trim());
    }
    this.updateProperty('interestedInProject', this.newProjectName.trim());
    this.newProjectName = '';
  }

  logout(): void { this.auth.logout(); }
}
