export interface Pagination{
    currentPage:number,
    pageSize:number,
    totalPages:number,
    totalCount:number
}

export class PagedList<T> {
    items:T;
    pagination:Pagination;
}