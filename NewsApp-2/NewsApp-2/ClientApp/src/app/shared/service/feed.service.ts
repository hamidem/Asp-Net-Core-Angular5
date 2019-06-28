import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Feed } from '../model/Feed';

@Injectable()
export class FeedService {

    http: HttpClient;
    baseUrl: string;
    public article: Feed;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
    }

    getAllArticles() {
        return this.http.get<Feed[]>(this.baseUrl + 'api/Article/GetRandArticles/');
    }

    getArticles(cat: number, id: number) {
        return this.http.get<Feed[]>(this.baseUrl + 'api/Article/GetArticles/' + cat + '/' + id);
    }
}
