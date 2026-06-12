import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../../shared/services/api.service';

export interface DashboardOverview {
  totalFarms: number;
  totalArea: number;
  averageHealthScore: number;
  activecrops: number;
}

export interface Analytics {
  yieldTrend: any[];
  healthTrend: any[];
  soilMetrics: any;
}

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  constructor(private apiService: ApiService) { }

  getOverview(): Observable<DashboardOverview> {
    return this.apiService.get<DashboardOverview>('/dashboard/overview');
  }

  getAnalytics(): Observable<Analytics> {
    return this.apiService.get<Analytics>('/dashboard/analytics');
  }

  getActivities(): Observable<any[]> {
    return this.apiService.get<any[]>('/dashboard/activities');
  }

  getFarms(): Observable<any[]> {
    return this.apiService.get<any[]>('/farms');
  }

  getFarmById(id: number): Observable<any> {
    return this.apiService.get<any>(`/farms/${id}`);
  }

  createFarm(farm: any): Observable<any> {
    return this.apiService.post<any>('/farms', farm);
  }

  updateFarm(id: number, farm: any): Observable<any> {
    return this.apiService.put<any>(`/farms/${id}`, farm);
  }

  deleteFarm(id: number): Observable<any> {
    return this.apiService.delete<any>(`/farms/${id}`);
  }
}
