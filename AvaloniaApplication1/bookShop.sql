PGDMP  6                    |            bookShop    16.1    16.1 U    D           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            E           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            F           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            G           1262    16857    bookShop    DATABASE     �   CREATE DATABASE "bookShop" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'English_United States.1251';
    DROP DATABASE "bookShop";
                postgres    false            �            1259    16960    authors    TABLE     s   CREATE TABLE public.authors (
    id bigint NOT NULL,
    name text,
    deleted boolean DEFAULT false NOT NULL
);
    DROP TABLE public.authors;
       public         heap    postgres    false            �            1259    16952    books    TABLE     1  CREATE TABLE public.books (
    id bigint NOT NULL,
    authorid integer NOT NULL,
    publisherid integer NOT NULL,
    price numeric NOT NULL,
    locationid integer NOT NULL,
    name text NOT NULL,
    article integer NOT NULL,
    barcode text NOT NULL,
    deleted boolean DEFAULT false NOT NULL
);
    DROP TABLE public.books;
       public         heap    postgres    false            �            1259    16994    booksgenres    TABLE     _   CREATE TABLE public.booksgenres (
    bookid integer NOT NULL,
    genreid integer NOT NULL
);
    DROP TABLE public.booksgenres;
       public         heap    postgres    false            �            1259    16974    genre    TABLE     q   CREATE TABLE public.genre (
    id bigint NOT NULL,
    name text,
    deleted boolean DEFAULT false NOT NULL
);
    DROP TABLE public.genre;
       public         heap    postgres    false            �            1259    16981 
   orderlines    TABLE     x   CREATE TABLE public.orderlines (
    id bigint NOT NULL,
    orderid integer,
    bookid integer,
    amount integer
);
    DROP TABLE public.orderlines;
       public         heap    postgres    false            �            1259    17263    author_genre_stats    VIEW       CREATE VIEW public.author_genre_stats AS
 SELECT au.name AS author_name,
    ge.name AS genre_name,
    sum(ol.amount) AS am,
    sum(((ol.amount)::numeric * b.price)) AS price
   FROM ((((public.orderlines ol
     JOIN public.books b ON ((ol.bookid = b.id)))
     JOIN public.authors au ON ((b.authorid = au.id)))
     JOIN public.booksgenres bg ON ((b.id = bg.bookid)))
     JOIN public.genre ge ON ((bg.genreid = ge.id)))
  GROUP BY au.name, ge.name
  ORDER BY (sum(ol.amount)) DESC, (sum(((ol.amount)::numeric * b.price))) DESC;
 %   DROP VIEW public.author_genre_stats;
       public          postgres    false    218    218    216    216    227    227    224    216    224    222    222            �            1259    16959    author_id_seq    SEQUENCE     v   CREATE SEQUENCE public.author_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 $   DROP SEQUENCE public.author_id_seq;
       public          postgres    false    218            H           0    0    author_id_seq    SEQUENCE OWNED BY     @   ALTER SEQUENCE public.author_id_seq OWNED BY public.authors.id;
          public          postgres    false    217            �            1259    17268    author_stats    VIEW       CREATE VIEW public.author_stats AS
 SELECT au.name AS author_name,
    sum(ol.amount) AS amount,
    sum(((ol.amount)::numeric * b.price)) AS price
   FROM ((public.orderlines ol
     JOIN public.books b ON ((ol.bookid = b.id)))
     JOIN public.authors au ON ((b.authorid = au.id)))
  GROUP BY au.name
  ORDER BY (sum(ol.amount)) DESC, (sum(((ol.amount)::numeric * b.price))) DESC;
    DROP VIEW public.author_stats;
       public          postgres    false    216    224    224    218    218    216    216            �            1259    17273 
   book_stats    VIEW     A  CREATE VIEW public.book_stats AS
 SELECT b.name AS book_name,
    sum(ol.amount) AS amount,
    sum(((ol.amount)::numeric * b.price)) AS price
   FROM (public.orderlines ol
     JOIN public.books b ON ((ol.bookid = b.id)))
  GROUP BY b.name
  ORDER BY (sum(ol.amount)) DESC, (sum(((ol.amount)::numeric * b.price))) DESC;
    DROP VIEW public.book_stats;
       public          postgres    false    216    216    216    224    224            �            1259    16950    books_id_seq    SEQUENCE     u   CREATE SEQUENCE public.books_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 #   DROP SEQUENCE public.books_id_seq;
       public          postgres    false    216            I           0    0    books_id_seq    SEQUENCE OWNED BY     =   ALTER SEQUENCE public.books_id_seq OWNED BY public.books.id;
          public          postgres    false    215            �            1259    17042 	   customers    TABLE     �   CREATE TABLE public.customers (
    id bigint NOT NULL,
    fio text NOT NULL,
    bonuses integer,
    deleted boolean DEFAULT false NOT NULL
);
    DROP TABLE public.customers;
       public         heap    postgres    false            �            1259    16988    orders    TABLE     �   CREATE TABLE public.orders (
    id bigint NOT NULL,
    customerid integer NOT NULL,
    sum numeric,
    employeeid integer,
    deleted boolean DEFAULT false NOT NULL
);
    DROP TABLE public.orders;
       public         heap    postgres    false            �            1259    17278    customers_favourite_genres    VIEW     =  CREATE VIEW public.customers_favourite_genres AS
 SELECT cu.fio,
    ge.name,
    sum(ol.amount) AS am,
    sum(((ol.amount)::numeric * b.price)) AS price
   FROM (((((public.orderlines ol
     JOIN public.books b ON ((ol.bookid = b.id)))
     JOIN public.orders ord ON ((ol.orderid = ord.id)))
     JOIN public.customers cu ON ((ord.customerid = cu.id)))
     JOIN public.booksgenres bg ON ((b.id = bg.bookid)))
     JOIN public.genre ge ON ((bg.genreid = ge.id)))
  GROUP BY cu.fio, ge.name
  ORDER BY (sum(ol.amount)) DESC, (sum(((ol.amount)::numeric * b.price))) DESC;
 -   DROP VIEW public.customers_favourite_genres;
       public          postgres    false    224    216    231    231    227    227    216    222    222    226    226    224    224            �            1259    17041    customers_id_seq    SEQUENCE     y   CREATE SEQUENCE public.customers_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 '   DROP SEQUENCE public.customers_id_seq;
       public          postgres    false    231            J           0    0    customers_id_seq    SEQUENCE OWNED BY     E   ALTER SEQUENCE public.customers_id_seq OWNED BY public.customers.id;
          public          postgres    false    230            �            1259    17057 	   employees    TABLE     �   CREATE TABLE public.employees (
    id bigint NOT NULL,
    fio text,
    rating integer,
    deleted boolean DEFAULT false NOT NULL,
    salary numeric
);
    DROP TABLE public.employees;
       public         heap    postgres    false            �            1259    16973    genre_id_seq    SEQUENCE     u   CREATE SEQUENCE public.genre_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 #   DROP SEQUENCE public.genre_id_seq;
       public          postgres    false    222            K           0    0    genre_id_seq    SEQUENCE OWNED BY     =   ALTER SEQUENCE public.genre_id_seq OWNED BY public.genre.id;
          public          postgres    false    221            �            1259    17283    genre_stats    VIEW     �  CREATE VIEW public.genre_stats AS
 SELECT ge.name,
    sum(ol.amount) AS am,
    sum(((ol.amount)::numeric * b.price)) AS price
   FROM (((public.orderlines ol
     JOIN public.books b ON ((ol.bookid = b.id)))
     JOIN public.booksgenres bg ON ((b.id = bg.bookid)))
     JOIN public.genre ge ON ((bg.genreid = ge.id)))
  GROUP BY ge.name
  ORDER BY (sum(ol.amount)) DESC, (sum(((ol.amount)::numeric * b.price))) DESC;
    DROP VIEW public.genre_stats;
       public          postgres    false    227    227    224    224    222    222    216    216            �            1259    17288 
   orders_sum    VIEW     T   CREATE VIEW public.orders_sum AS
 SELECT sum(sum) AS sum
   FROM public.orders ors;
    DROP VIEW public.orders_sum;
       public          postgres    false    226            �            1259    17221    receipt    TABLE     �   CREATE TABLE public.receipt (
    id integer NOT NULL,
    bookid integer NOT NULL,
    amount_of_books integer NOT NULL,
    sum numeric NOT NULL,
    deleted boolean NOT NULL
);
    DROP TABLE public.receipt;
       public         heap    postgres    false            �            1259    17292    sum_receipt    VIEW     U   CREATE VIEW public.sum_receipt AS
 SELECT sum(sum) AS sum
   FROM public.receipt re;
    DROP VIEW public.sum_receipt;
       public          postgres    false    235            �            1259    17296    income_and_outcome_this_month    VIEW     �  CREATE VIEW public.income_and_outcome_this_month AS
 SELECT (sum(salary) + ( SELECT sum_receipt.sum
           FROM public.sum_receipt)) AS outcome,
    ( SELECT orders_sum.sum
           FROM public.orders_sum) AS income,
    ((( SELECT orders_sum.sum
           FROM public.orders_sum) - sum(salary)) - ( SELECT sum_receipt.sum
           FROM public.sum_receipt)) AS fin
   FROM public.employees emp;
 0   DROP VIEW public.income_and_outcome_this_month;
       public          postgres    false    241    242    233            �            1259    17000    location    TABLE     t   CREATE TABLE public.location (
    id bigint NOT NULL,
    name text,
    deleted boolean DEFAULT false NOT NULL
);
    DROP TABLE public.location;
       public         heap    postgres    false            �            1259    16999    location_id_seq    SEQUENCE     x   CREATE SEQUENCE public.location_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 &   DROP SEQUENCE public.location_id_seq;
       public          postgres    false    229            L           0    0    location_id_seq    SEQUENCE OWNED BY     C   ALTER SEQUENCE public.location_id_seq OWNED BY public.location.id;
          public          postgres    false    228            �            1259    16980    orderlines_id_seq    SEQUENCE     z   CREATE SEQUENCE public.orderlines_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 (   DROP SEQUENCE public.orderlines_id_seq;
       public          postgres    false    224            M           0    0    orderlines_id_seq    SEQUENCE OWNED BY     G   ALTER SEQUENCE public.orderlines_id_seq OWNED BY public.orderlines.id;
          public          postgres    false    223            �            1259    16987    orders_id_seq    SEQUENCE     v   CREATE SEQUENCE public.orders_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 $   DROP SEQUENCE public.orders_id_seq;
       public          postgres    false    226            N           0    0    orders_id_seq    SEQUENCE OWNED BY     ?   ALTER SEQUENCE public.orders_id_seq OWNED BY public.orders.id;
          public          postgres    false    225            �            1259    16967 	   publisher    TABLE     u   CREATE TABLE public.publisher (
    id bigint NOT NULL,
    name text,
    deleted boolean DEFAULT false NOT NULL
);
    DROP TABLE public.publisher;
       public         heap    postgres    false            �            1259    16966    publisher_id_seq    SEQUENCE     y   CREATE SEQUENCE public.publisher_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 '   DROP SEQUENCE public.publisher_id_seq;
       public          postgres    false    220            O           0    0    publisher_id_seq    SEQUENCE OWNED BY     E   ALTER SEQUENCE public.publisher_id_seq OWNED BY public.publisher.id;
          public          postgres    false    219            �            1259    17220    receipt_id_seq    SEQUENCE     �   CREATE SEQUENCE public.receipt_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 %   DROP SEQUENCE public.receipt_id_seq;
       public          postgres    false    235            P           0    0    receipt_id_seq    SEQUENCE OWNED BY     A   ALTER SEQUENCE public.receipt_id_seq OWNED BY public.receipt.id;
          public          postgres    false    234            �            1259    17056    workers_id_seq    SEQUENCE     w   CREATE SEQUENCE public.workers_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 %   DROP SEQUENCE public.workers_id_seq;
       public          postgres    false    233            Q           0    0    workers_id_seq    SEQUENCE OWNED BY     C   ALTER SEQUENCE public.workers_id_seq OWNED BY public.employees.id;
          public          postgres    false    232            [           2604    16963 
   authors id    DEFAULT     g   ALTER TABLE ONLY public.authors ALTER COLUMN id SET DEFAULT nextval('public.author_id_seq'::regclass);
 9   ALTER TABLE public.authors ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    218    217    218            Y           2604    16955    books id    DEFAULT     d   ALTER TABLE ONLY public.books ALTER COLUMN id SET DEFAULT nextval('public.books_id_seq'::regclass);
 7   ALTER TABLE public.books ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    216    215    216            f           2604    17045    customers id    DEFAULT     l   ALTER TABLE ONLY public.customers ALTER COLUMN id SET DEFAULT nextval('public.customers_id_seq'::regclass);
 ;   ALTER TABLE public.customers ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    231    230    231            h           2604    17060    employees id    DEFAULT     j   ALTER TABLE ONLY public.employees ALTER COLUMN id SET DEFAULT nextval('public.workers_id_seq'::regclass);
 ;   ALTER TABLE public.employees ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    233    232    233            _           2604    16977    genre id    DEFAULT     d   ALTER TABLE ONLY public.genre ALTER COLUMN id SET DEFAULT nextval('public.genre_id_seq'::regclass);
 7   ALTER TABLE public.genre ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    221    222    222            d           2604    17003    location id    DEFAULT     j   ALTER TABLE ONLY public.location ALTER COLUMN id SET DEFAULT nextval('public.location_id_seq'::regclass);
 :   ALTER TABLE public.location ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    228    229    229            a           2604    16984    orderlines id    DEFAULT     n   ALTER TABLE ONLY public.orderlines ALTER COLUMN id SET DEFAULT nextval('public.orderlines_id_seq'::regclass);
 <   ALTER TABLE public.orderlines ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    223    224    224            b           2604    16991 	   orders id    DEFAULT     f   ALTER TABLE ONLY public.orders ALTER COLUMN id SET DEFAULT nextval('public.orders_id_seq'::regclass);
 8   ALTER TABLE public.orders ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    226    225    226            ]           2604    16970    publisher id    DEFAULT     l   ALTER TABLE ONLY public.publisher ALTER COLUMN id SET DEFAULT nextval('public.publisher_id_seq'::regclass);
 ;   ALTER TABLE public.publisher ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    219    220    220            j           2604    17224 
   receipt id    DEFAULT     h   ALTER TABLE ONLY public.receipt ALTER COLUMN id SET DEFAULT nextval('public.receipt_id_seq'::regclass);
 9   ALTER TABLE public.receipt ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    234    235    235            n           2606    16965    authors author_pkey 
   CONSTRAINT     Q   ALTER TABLE ONLY public.authors
    ADD CONSTRAINT author_pkey PRIMARY KEY (id);
 =   ALTER TABLE ONLY public.authors DROP CONSTRAINT author_pkey;
       public            postgres    false    218            l           2606    16958    books books_pkey 
   CONSTRAINT     N   ALTER TABLE ONLY public.books
    ADD CONSTRAINT books_pkey PRIMARY KEY (id);
 :   ALTER TABLE ONLY public.books DROP CONSTRAINT books_pkey;
       public            postgres    false    216            z           2606    16998    booksgenres booksgenres_pkey 
   CONSTRAINT     g   ALTER TABLE ONLY public.booksgenres
    ADD CONSTRAINT booksgenres_pkey PRIMARY KEY (bookid, genreid);
 F   ALTER TABLE ONLY public.booksgenres DROP CONSTRAINT booksgenres_pkey;
       public            postgres    false    227    227            ~           2606    17047    customers customers_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public.customers
    ADD CONSTRAINT customers_pkey PRIMARY KEY (id);
 B   ALTER TABLE ONLY public.customers DROP CONSTRAINT customers_pkey;
       public            postgres    false    231            r           2606    16979    genre genre_pkey 
   CONSTRAINT     N   ALTER TABLE ONLY public.genre
    ADD CONSTRAINT genre_pkey PRIMARY KEY (id);
 :   ALTER TABLE ONLY public.genre DROP CONSTRAINT genre_pkey;
       public            postgres    false    222            |           2606    17005    location location_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public.location
    ADD CONSTRAINT location_pkey PRIMARY KEY (id);
 @   ALTER TABLE ONLY public.location DROP CONSTRAINT location_pkey;
       public            postgres    false    229            t           2606    16986    orderlines orderlines_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public.orderlines
    ADD CONSTRAINT orderlines_pkey PRIMARY KEY (id);
 D   ALTER TABLE ONLY public.orderlines DROP CONSTRAINT orderlines_pkey;
       public            postgres    false    224            x           2606    16993    orders orders_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_pkey PRIMARY KEY (id);
 <   ALTER TABLE ONLY public.orders DROP CONSTRAINT orders_pkey;
       public            postgres    false    226            p           2606    16972    publisher publisher_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public.publisher
    ADD CONSTRAINT publisher_pkey PRIMARY KEY (id);
 B   ALTER TABLE ONLY public.publisher DROP CONSTRAINT publisher_pkey;
       public            postgres    false    220            �           2606    17226    receipt receipt_pkey 
   CONSTRAINT     R   ALTER TABLE ONLY public.receipt
    ADD CONSTRAINT receipt_pkey PRIMARY KEY (id);
 >   ALTER TABLE ONLY public.receipt DROP CONSTRAINT receipt_pkey;
       public            postgres    false    235            �           2606    17064    employees workers_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public.employees
    ADD CONSTRAINT workers_pkey PRIMARY KEY (id);
 @   ALTER TABLE ONLY public.employees DROP CONSTRAINT workers_pkey;
       public            postgres    false    233            u           1259    17055    fki_customerId_fkey    INDEX     N   CREATE INDEX "fki_customerId_fkey" ON public.orders USING btree (customerid);
 )   DROP INDEX public."fki_customerId_fkey";
       public            postgres    false    226            v           1259    17070    fki_employeeId_fkey    INDEX     N   CREATE INDEX "fki_employeeId_fkey" ON public.orders USING btree (employeeid);
 )   DROP INDEX public."fki_employeeId_fkey";
       public            postgres    false    226            �           1259    17262    fki_receipt_bookid_fkey    INDEX     M   CREATE INDEX fki_receipt_bookid_fkey ON public.receipt USING btree (bookid);
 +   DROP INDEX public.fki_receipt_bookid_fkey;
       public            postgres    false    235            �           2620    17122    authors authors    TRIGGER     s   CREATE TRIGGER authors AFTER INSERT OR UPDATE ON public.authors FOR EACH ROW EXECUTE FUNCTION public.my_notifys();
 (   DROP TRIGGER authors ON public.authors;
       public          postgres    false    218            �           2620    17121    books books    TRIGGER     o   CREATE TRIGGER books AFTER INSERT OR UPDATE ON public.books FOR EACH ROW EXECUTE FUNCTION public.my_notifys();
 $   DROP TRIGGER books ON public.books;
       public          postgres    false    216            �           2620    17124    customers customers    TRIGGER     w   CREATE TRIGGER customers AFTER INSERT OR UPDATE ON public.customers FOR EACH ROW EXECUTE FUNCTION public.my_notifys();
 ,   DROP TRIGGER customers ON public.customers;
       public          postgres    false    231            �           2620    17125    employees employees    TRIGGER     w   CREATE TRIGGER employees AFTER INSERT OR UPDATE ON public.employees FOR EACH ROW EXECUTE FUNCTION public.my_notifys();
 ,   DROP TRIGGER employees ON public.employees;
       public          postgres    false    233            �           2620    17123    genre genre    TRIGGER     o   CREATE TRIGGER genre AFTER INSERT OR UPDATE ON public.genre FOR EACH ROW EXECUTE FUNCTION public.my_notifys();
 $   DROP TRIGGER genre ON public.genre;
       public          postgres    false    222            �           2620    17127    location location    TRIGGER     u   CREATE TRIGGER location AFTER INSERT OR UPDATE ON public.location FOR EACH ROW EXECUTE FUNCTION public.my_notifys();
 *   DROP TRIGGER location ON public.location;
       public          postgres    false    229            �           2620    17128    orderlines orderlines    TRIGGER     y   CREATE TRIGGER orderlines AFTER INSERT OR UPDATE ON public.orderlines FOR EACH ROW EXECUTE FUNCTION public.my_notifys();
 .   DROP TRIGGER orderlines ON public.orderlines;
       public          postgres    false    224            �           2620    17126    publisher publisher    TRIGGER     w   CREATE TRIGGER publisher AFTER INSERT OR UPDATE ON public.publisher FOR EACH ROW EXECUTE FUNCTION public.my_notifys();
 ,   DROP TRIGGER publisher ON public.publisher;
       public          postgres    false    220            �           2606    17006    books books_authorid_fkey    FK CONSTRAINT     {   ALTER TABLE ONLY public.books
    ADD CONSTRAINT books_authorid_fkey FOREIGN KEY (authorid) REFERENCES public.authors(id);
 C   ALTER TABLE ONLY public.books DROP CONSTRAINT books_authorid_fkey;
       public          postgres    false    218    4718    216            �           2606    17016    books books_locationid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.books
    ADD CONSTRAINT books_locationid_fkey FOREIGN KEY (locationid) REFERENCES public.location(id);
 E   ALTER TABLE ONLY public.books DROP CONSTRAINT books_locationid_fkey;
       public          postgres    false    216    4732    229            �           2606    17011    books books_publisherid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.books
    ADD CONSTRAINT books_publisherid_fkey FOREIGN KEY (publisherid) REFERENCES public.publisher(id);
 F   ALTER TABLE ONLY public.books DROP CONSTRAINT books_publisherid_fkey;
       public          postgres    false    220    216    4720            �           2606    17031 #   booksgenres booksgenres_bookid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.booksgenres
    ADD CONSTRAINT booksgenres_bookid_fkey FOREIGN KEY (bookid) REFERENCES public.books(id);
 M   ALTER TABLE ONLY public.booksgenres DROP CONSTRAINT booksgenres_bookid_fkey;
       public          postgres    false    227    216    4716            �           2606    17036 $   booksgenres booksgenres_genreid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.booksgenres
    ADD CONSTRAINT booksgenres_genreid_fkey FOREIGN KEY (genreid) REFERENCES public.genre(id);
 N   ALTER TABLE ONLY public.booksgenres DROP CONSTRAINT booksgenres_genreid_fkey;
       public          postgres    false    222    227    4722            �           2606    17050    orders customerid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.orders
    ADD CONSTRAINT customerid_fkey FOREIGN KEY (customerid) REFERENCES public.customers(id) ON UPDATE CASCADE ON DELETE SET DEFAULT;
 @   ALTER TABLE ONLY public.orders DROP CONSTRAINT customerid_fkey;
       public          postgres    false    231    4734    226            �           2606    17071    orders employeeid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.orders
    ADD CONSTRAINT employeeid_fkey FOREIGN KEY (employeeid) REFERENCES public.employees(id) ON UPDATE CASCADE ON DELETE SET DEFAULT;
 @   ALTER TABLE ONLY public.orders DROP CONSTRAINT employeeid_fkey;
       public          postgres    false    4736    233    226            �           2606    17026 !   orderlines orderlines_bookid_fkey    FK CONSTRAINT        ALTER TABLE ONLY public.orderlines
    ADD CONSTRAINT orderlines_bookid_fkey FOREIGN KEY (bookid) REFERENCES public.books(id);
 K   ALTER TABLE ONLY public.orderlines DROP CONSTRAINT orderlines_bookid_fkey;
       public          postgres    false    4716    224    216            �           2606    17021 "   orderlines orderlines_orderid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.orderlines
    ADD CONSTRAINT orderlines_orderid_fkey FOREIGN KEY (orderid) REFERENCES public.orders(id);
 L   ALTER TABLE ONLY public.orderlines DROP CONSTRAINT orderlines_orderid_fkey;
       public          postgres    false    226    4728    224            �           2606    17257    receipt receipt_bookid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.receipt
    ADD CONSTRAINT receipt_bookid_fkey FOREIGN KEY (bookid) REFERENCES public.books(id) ON UPDATE RESTRICT ON DELETE RESTRICT;
 E   ALTER TABLE ONLY public.receipt DROP CONSTRAINT receipt_bookid_fkey;
       public          postgres    false    216    235    4716           