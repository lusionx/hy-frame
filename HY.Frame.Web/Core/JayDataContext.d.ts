﻿///<reference path="./jaydata.d.ts" />
/*//////////////////////////////////////////////////////////////////////////////////////
////// Autogenerated by JaySvcUtil.exe http://JayData.org for more info        /////////
//////                      oData  V2  TypeScript                              /////////
//////////////////////////////////////////////////////////////////////////////////////*/


module XXXX {
  class com_person extends $data.Entity {
    constructor ();
    constructor (initData: { com_person_id?: string; person_no?: string; Other_no?: string; person_name?: string; sex?: string; cert_id?: string; birthday?: Date; nation?: string; party?: string; university?: string; telephone?: string; mobile?: string; email?: string; spec?: string; job_date?: Date; education?: string; degree?: string; prof_cert_id?: string; hospital_duty?: string; photo?: string; person_type?: string; remark?: string; passwd?: string; Dept_id?: string; Update_other_no?: number; Transfer_state?: number; add_time?: Date; BatchGuid?: string; is_create_card?: bool; is_charges?: bool; internals?: string; update_time?: Date; });
    com_person_id: string;
    person_no: string;
    Other_no: string;
    person_name: string;
    sex: string;
    cert_id: string;
    birthday: Date;
    nation: string;
    party: string;
    university: string;
    telephone: string;
    mobile: string;
    email: string;
    spec: string;
    job_date: Date;
    education: string;
    degree: string;
    prof_cert_id: string;
    hospital_duty: string;
    photo: string;
    person_type: string;
    remark: string;
    passwd: string;
    Dept_id: string;
    Update_other_no: number;
    Transfer_state: number;
    add_time: Date;
    BatchGuid: string;
    is_create_card: bool;
    is_charges: bool;
    internals: string;
    update_time: Date;
    
  }

  export interface com_personQueryable extends $data.Queryable {
    filter(predicate:(it: XXXX.com_person) => bool): XXXX.com_personQueryable;
    filter(predicate:(it: XXXX.com_person) => bool, thisArg: any): XXXX.com_personQueryable;

    map(projection: (it: XXXX.com_person) => any): XXXX.com_personQueryable;

    length(): $data.IPromise;
    length(handler: (result: number) => void): $data.IPromise;
    length(handler: { success?: (result: number) => void; error?: (result: any) => void; }): $data.IPromise;

    forEach(handler: (it: XXXX.com_person) => void ): $data.IPromise;
    
    toArray(): $data.IPromise;
    toArray(handler: (result: XXXX.com_person[]) => void): $data.IPromise;
    toArray(handler: { success?: (result: XXXX.com_person[]) => void; error?: (result: any) => void; }): $data.IPromise;

    single(predicate: (it: XXXX.com_person, params?: any) => bool, params?: any, handler?: (result: XXXX.com_person) => void): $data.IPromise;
    single(predicate: (it: XXXX.com_person, params?: any) => bool, params?: any, handler?: { success?: (result: XXXX.com_person[]) => void; error?: (result: any) => void; }): $data.IPromise;

    take(amout: number): XXXX.com_personQueryable;
    skip(amout: number): XXXX.com_personQueryable;

    order(selector: string): XXXX.com_personQueryable;
    orderBy(predicate: (it: XXXX.com_person) => any): XXXX.com_personQueryable;
    orderByDescending(predicate: (it: XXXX.com_person) => any): XXXX.com_personQueryable;
    
    first(predicate: (it: XXXX.com_person, params?: any) => bool, params?: any, handler?: (result: XXXX.com_person) => void): $data.IPromise;
    first(predicate: (it: XXXX.com_person, params?: any) => bool, params?: any, handler?: { success?: (result: XXXX.com_person[]) => void; error?: (result: any) => void; }): $data.IPromise;
    
    include(selector: string): XXXX.com_personQueryable;
    withInlineCount(): XXXX.com_personQueryable;
    withInlineCount(selector: string): XXXX.com_personQueryable;

    removeAll(): $data.IPromise;
    removeAll(handler: (count: number) => void): $data.IPromise;
    removeAll(handler: { success?: (result: number) => void; error?: (result: any) => void; }): $data.IPromise;
  }


  export interface com_personSet extends com_personQueryable {
    add(initData: { com_person_id?: string; person_no?: string; Other_no?: string; person_name?: string; sex?: string; cert_id?: string; birthday?: Date; nation?: string; party?: string; university?: string; telephone?: string; mobile?: string; email?: string; spec?: string; job_date?: Date; education?: string; degree?: string; prof_cert_id?: string; hospital_duty?: string; photo?: string; person_type?: string; remark?: string; passwd?: string; Dept_id?: string; Update_other_no?: number; Transfer_state?: number; add_time?: Date; BatchGuid?: string; is_create_card?: bool; is_charges?: bool; internals?: string; update_time?: Date; }): XXXX.com_person;
    add(item: XXXX.com_person): XXXX.com_person;
    addMany(items: { com_person_id?: string; person_no?: string; Other_no?: string; person_name?: string; sex?: string; cert_id?: string; birthday?: Date; nation?: string; party?: string; university?: string; telephone?: string; mobile?: string; email?: string; spec?: string; job_date?: Date; education?: string; degree?: string; prof_cert_id?: string; hospital_duty?: string; photo?: string; person_type?: string; remark?: string; passwd?: string; Dept_id?: string; Update_other_no?: number; Transfer_state?: number; add_time?: Date; BatchGuid?: string; is_create_card?: bool; is_charges?: bool; internals?: string; update_time?: Date; }[]): XXXX.com_person[];
    addMany(items: XXXX.com_person[]): XXXX.com_person[];
  
    attach(item: XXXX.com_person): void;
    attach(item: { com_person_id: string; }): void;
    attachOrGet(item: XXXX.com_person): XXXX.com_person;
    attachOrGet(item: { com_person_id: string; }): XXXX.com_person;

    detach(item: XXXX.com_person): void;
    detach(item: { com_person_id: string; }): void;

    remove(item: XXXX.com_person): void;
    remove(item: { com_person_id: string; }): void;
    
    elementType: new (initData: { com_person_id?: string; person_no?: string; Other_no?: string; person_name?: string; sex?: string; cert_id?: string; birthday?: Date; nation?: string; party?: string; university?: string; telephone?: string; mobile?: string; email?: string; spec?: string; job_date?: Date; education?: string; degree?: string; prof_cert_id?: string; hospital_duty?: string; photo?: string; person_type?: string; remark?: string; passwd?: string; Dept_id?: string; Update_other_no?: number; Transfer_state?: number; add_time?: Date; BatchGuid?: string; is_create_card?: bool; is_charges?: bool; internals?: string; update_time?: Date; }) => XXXX.com_person;
    
    
  }

  class com_person_view extends $data.Entity {
    constructor ();
    constructor (initData: { com_person_id?: string; person_no?: string; person_name?: string; sex?: string; birthday?: Date; Dept_id?: string; dept_name?: string; title_name?: string; title_id?: string; unit?: string; person_state_name?: string; Other_no?: string; cert_id?: string; nation?: string; party?: string; university?: string; telephone?: string; mobile?: string; email?: string; spec?: string; job_date?: Date; education?: string; degree?: string; prof_cert_id?: string; hospital_duty?: string; photo?: string; person_type?: string; remark?: string; passwd?: string; add_time?: Date; Transfer_state?: number; nation_name?: string; party_name?: string; Spec_name?: string; education_name?: string; Degree_name?: string; hospital_duty_name?: string; internals?: string; unit_name?: string; });
    com_person_id: string;
    person_no: string;
    person_name: string;
    sex: string;
    birthday: Date;
    Dept_id: string;
    dept_name: string;
    title_name: string;
    title_id: string;
    unit: string;
    person_state_name: string;
    Other_no: string;
    cert_id: string;
    nation: string;
    party: string;
    university: string;
    telephone: string;
    mobile: string;
    email: string;
    spec: string;
    job_date: Date;
    education: string;
    degree: string;
    prof_cert_id: string;
    hospital_duty: string;
    photo: string;
    person_type: string;
    remark: string;
    passwd: string;
    add_time: Date;
    Transfer_state: number;
    nation_name: string;
    party_name: string;
    Spec_name: string;
    education_name: string;
    Degree_name: string;
    hospital_duty_name: string;
    internals: string;
    unit_name: string;
    
  }

  export interface com_person_viewQueryable extends $data.Queryable {
    filter(predicate:(it: XXXX.com_person_view) => bool): XXXX.com_person_viewQueryable;
    filter(predicate:(it: XXXX.com_person_view) => bool, thisArg: any): XXXX.com_person_viewQueryable;

    map(projection: (it: XXXX.com_person_view) => any): XXXX.com_person_viewQueryable;

    length(): $data.IPromise;
    length(handler: (result: number) => void): $data.IPromise;
    length(handler: { success?: (result: number) => void; error?: (result: any) => void; }): $data.IPromise;

    forEach(handler: (it: XXXX.com_person_view) => void ): $data.IPromise;
    
    toArray(): $data.IPromise;
    toArray(handler: (result: XXXX.com_person_view[]) => void): $data.IPromise;
    toArray(handler: { success?: (result: XXXX.com_person_view[]) => void; error?: (result: any) => void; }): $data.IPromise;

    single(predicate: (it: XXXX.com_person_view, params?: any) => bool, params?: any, handler?: (result: XXXX.com_person_view) => void): $data.IPromise;
    single(predicate: (it: XXXX.com_person_view, params?: any) => bool, params?: any, handler?: { success?: (result: XXXX.com_person_view[]) => void; error?: (result: any) => void; }): $data.IPromise;

    take(amout: number): XXXX.com_person_viewQueryable;
    skip(amout: number): XXXX.com_person_viewQueryable;

    order(selector: string): XXXX.com_person_viewQueryable;
    orderBy(predicate: (it: XXXX.com_person_view) => any): XXXX.com_person_viewQueryable;
    orderByDescending(predicate: (it: XXXX.com_person_view) => any): XXXX.com_person_viewQueryable;
    
    first(predicate: (it: XXXX.com_person_view, params?: any) => bool, params?: any, handler?: (result: XXXX.com_person_view) => void): $data.IPromise;
    first(predicate: (it: XXXX.com_person_view, params?: any) => bool, params?: any, handler?: { success?: (result: XXXX.com_person_view[]) => void; error?: (result: any) => void; }): $data.IPromise;
    
    include(selector: string): XXXX.com_person_viewQueryable;
    withInlineCount(): XXXX.com_person_viewQueryable;
    withInlineCount(selector: string): XXXX.com_person_viewQueryable;

    removeAll(): $data.IPromise;
    removeAll(handler: (count: number) => void): $data.IPromise;
    removeAll(handler: { success?: (result: number) => void; error?: (result: any) => void; }): $data.IPromise;
  }


  export interface com_person_viewSet extends com_person_viewQueryable {
    add(initData: { com_person_id?: string; person_no?: string; person_name?: string; sex?: string; birthday?: Date; Dept_id?: string; dept_name?: string; title_name?: string; title_id?: string; unit?: string; person_state_name?: string; Other_no?: string; cert_id?: string; nation?: string; party?: string; university?: string; telephone?: string; mobile?: string; email?: string; spec?: string; job_date?: Date; education?: string; degree?: string; prof_cert_id?: string; hospital_duty?: string; photo?: string; person_type?: string; remark?: string; passwd?: string; add_time?: Date; Transfer_state?: number; nation_name?: string; party_name?: string; Spec_name?: string; education_name?: string; Degree_name?: string; hospital_duty_name?: string; internals?: string; unit_name?: string; }): XXXX.com_person_view;
    add(item: XXXX.com_person_view): XXXX.com_person_view;
    addMany(items: { com_person_id?: string; person_no?: string; person_name?: string; sex?: string; birthday?: Date; Dept_id?: string; dept_name?: string; title_name?: string; title_id?: string; unit?: string; person_state_name?: string; Other_no?: string; cert_id?: string; nation?: string; party?: string; university?: string; telephone?: string; mobile?: string; email?: string; spec?: string; job_date?: Date; education?: string; degree?: string; prof_cert_id?: string; hospital_duty?: string; photo?: string; person_type?: string; remark?: string; passwd?: string; add_time?: Date; Transfer_state?: number; nation_name?: string; party_name?: string; Spec_name?: string; education_name?: string; Degree_name?: string; hospital_duty_name?: string; internals?: string; unit_name?: string; }[]): XXXX.com_person_view[];
    addMany(items: XXXX.com_person_view[]): XXXX.com_person_view[];
  
    attach(item: XXXX.com_person_view): void;
    attach(item: { com_person_id: string; person_no: string; person_name: string; sex: string; birthday: Date; Dept_id: string; Other_no: string; cert_id: string; university: string; telephone: string; mobile: string; email: string; job_date: Date; prof_cert_id: string; photo: string; remark: string; passwd: string; add_time: Date; Transfer_state: number; internals: string; }): void;
    attachOrGet(item: XXXX.com_person_view): XXXX.com_person_view;
    attachOrGet(item: { com_person_id: string; person_no: string; person_name: string; sex: string; birthday: Date; Dept_id: string; Other_no: string; cert_id: string; university: string; telephone: string; mobile: string; email: string; job_date: Date; prof_cert_id: string; photo: string; remark: string; passwd: string; add_time: Date; Transfer_state: number; internals: string; }): XXXX.com_person_view;

    detach(item: XXXX.com_person_view): void;
    detach(item: { com_person_id: string; person_no: string; person_name: string; sex: string; birthday: Date; Dept_id: string; Other_no: string; cert_id: string; university: string; telephone: string; mobile: string; email: string; job_date: Date; prof_cert_id: string; photo: string; remark: string; passwd: string; add_time: Date; Transfer_state: number; internals: string; }): void;

    remove(item: XXXX.com_person_view): void;
    remove(item: { com_person_id: string; person_no: string; person_name: string; sex: string; birthday: Date; Dept_id: string; Other_no: string; cert_id: string; university: string; telephone: string; mobile: string; email: string; job_date: Date; prof_cert_id: string; photo: string; remark: string; passwd: string; add_time: Date; Transfer_state: number; internals: string; }): void;
    
    elementType: new (initData: { com_person_id?: string; person_no?: string; person_name?: string; sex?: string; birthday?: Date; Dept_id?: string; dept_name?: string; title_name?: string; title_id?: string; unit?: string; person_state_name?: string; Other_no?: string; cert_id?: string; nation?: string; party?: string; university?: string; telephone?: string; mobile?: string; email?: string; spec?: string; job_date?: Date; education?: string; degree?: string; prof_cert_id?: string; hospital_duty?: string; photo?: string; person_type?: string; remark?: string; passwd?: string; add_time?: Date; Transfer_state?: number; nation_name?: string; party_name?: string; Spec_name?: string; education_name?: string; Degree_name?: string; hospital_duty_name?: string; internals?: string; unit_name?: string; }) => XXXX.com_person_view;
    
    
  }

}

module HY.Frame.Bis {
  export class DBEntities extends $data.EntityContext {
    onReady(): $data.IPromise;
    onReady(handler: (context: DBEntities) => void): $data.IPromise;
    com_person: XXXX.com_personSet;
    com_person_view: XXXX.com_person_viewSet;
    
  }
}
