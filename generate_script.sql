create table department
(
    id     serial      not null
        constraint table_name_pk
            primary key,
    number integer     not null,
    name   varchar(30) not null,
    phone  varchar(15) not null
);

alter table department
    owner to postgres;

create unique index table_name_name_uindex
    on department (name);

create unique index table_name_number_uindex
    on department (number);

create unique index table_name_phone_uindex
    on department (phone);

create table workers
(
    id            serial       not null
        constraint workers_pk
            primary key,
    name          varchar(100) not null,
    department_id integer      not null
        constraint workers_department_id_fk
            references department,
    pg_user_id    integer      not null
);

alter table workers
    owner to postgres;

create unique index workers_id_uindex
    on workers (id);

create unique index workers_pg_user_id_uindex
    on workers (pg_user_id);

create table document_theme
(
    id   serial       not null
        constraint document_theme_pk
            primary key,
    name varchar(100) not null
);

alter table document_theme
    owner to postgres;

create unique index document_theme_id_uindex
    on document_theme (id);

create unique index document_theme_name_uindex
    on document_theme (name);

create table rack
(
    id     serial  not null
        constraint rack_pk
            primary key,
    number integer not null
);

alter table rack
    owner to postgres;

create unique index rack_id_uindex
    on rack (id);

create unique index rack_number_uindex
    on rack (number);

create table shelf
(
    id      serial  not null
        constraint shelf_pk
            primary key,
    number  integer,
    rack_id integer not null
        constraint shelf_rack_id_fk
            references rack
);

alter table shelf
    owner to postgres;

create unique index shelf_id_uindex
    on shelf (id);

create unique index shelf_number_rack_id_uindex
    on shelf (number, rack_id);

create table cell
(
    id       serial  not null
        constraint cell_pk
            primary key,
    number   integer not null,
    shelf_id integer not null
        constraint cell_shelf_id_fk
            references shelf
);

alter table cell
    owner to postgres;

create unique index cell_id_uindex
    on cell (id);

create unique index cell_number_shelf_id_uindex
    on cell (number, shelf_id);

create table document
(
    id           serial       not null
        constraint document_pk
            primary key,
    name         varchar(100) not null,
    theme_id     integer      not null
        constraint document_document_theme_id_fk
            references document_theme
            deferrable initially deferred,
    number       integer      not null,
    receipt_date timestamp with time zone,
    count        integer      not null,
    cell_id      integer      not null
        constraint document_cell_id_fk
            references cell
);

alter table document
    owner to postgres;

create unique index document_id_uindex
    on document (id);

create unique index document_cell_id_uindex
    on document (cell_id);

create table requests
(
    id           serial                   not null
        constraint requests_pk
            primary key,
    worker_id    integer                  not null
        constraint requests_workers_id_fk
            references workers,
    document_id  integer                  not null
        constraint requests_document_id_fk
            references document,
    request_date timestamp with time zone not null
);

alter table requests
    owner to postgres;

create unique index requests_id_uindex
    on requests (id);

