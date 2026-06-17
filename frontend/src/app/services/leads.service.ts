import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { AuthService } from './auth.service';

export interface Lead {
  id: number;
  contactName: string;
  phone: string;
  email: string;
  source: string;
  budget: string;
  area: string;
  rooms: string;
  propertyType: string;
  floor: string;
  financing: string;
  timeline: string;
  intent: string;
  amenities: string;
  airDirections: number | null;
  nearBy: string;
  objections: string;
  referralProject: string;
  interestedInProject: string;
  rating: string;
  status: string;
  assignedToId: number | null;
  assignedTo?: { id: number; name: string; role: string } | null;
  assignedToName?: string;
  transcript: string;
  notes: string;
  createdAt: string;
  updatedAt: string;
}

export interface Agent {
  id: number;
  name: string;
  role: string;
}

@Injectable({ providedIn: 'root' })
export class LeadsService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient, private auth: AuthService) {}

  private get headers(): HttpHeaders {
    return new HttpHeaders({ Authorization: `Bearer ${this.auth.token}` });
  }

  getAllLeads(): Observable<Lead[]> {
    return this.http.get<Lead[]>(`${this.apiUrl}/leads`, { headers: this.headers });
  }

  getMyLeads(): Observable<Lead[]> {
    return this.http.get<Lead[]>(`${this.apiUrl}/leads/my`, { headers: this.headers });
  }

  getAllLeadsForAgent(): Observable<Lead[]> {
    return this.http.get<Lead[]>(`${this.apiUrl}/leads/all-view`, { headers: this.headers });
  }

  updateLead(id: number, data: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/leads/${id}`, data, { headers: this.headers });
  }

  assignLead(leadId: number, agentId: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/leads/${leadId}/assign`, { agentId }, { headers: this.headers });
  }

  deleteLead(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/leads/${id}`, { headers: this.headers });
  }

  addPhoneCall(leadId: number, agent: string, title: string, summary: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/leads/${leadId}/phone-call`, { agent, title, summary }, { headers: this.headers });
  }

  getAgents(): Observable<Agent[]> {
    return this.http.get<Agent[]>(`${this.apiUrl}/agents`, { headers: this.headers });
  }

  getProjects(): Observable<{ id: number; name: string }[]> {
    return this.http.get<{ id: number; name: string }[]>(`${this.apiUrl}/projects`, { headers: this.headers });
  }

  addProject(name: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/projects`, { name }, { headers: this.headers });
  }

  getAmenityOptions(): Observable<{ id: number; name: string }[]> {
    return this.http.get<{ id: number; name: string }[]>(`${this.apiUrl}/options/amenities`, { headers: this.headers });
  }

  addAmenityOption(name: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/options/amenities`, { name }, { headers: this.headers });
  }

  getNearByOptions(): Observable<{ id: number; name: string }[]> {
    return this.http.get<{ id: number; name: string }[]>(`${this.apiUrl}/options/nearby`, { headers: this.headers });
  }

  addNearByOption(name: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/options/nearby`, { name }, { headers: this.headers });
  }
}
