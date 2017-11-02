
 
 
 create table t1(name varchar(5))
 create table t2(name varchar(5))
 
 insert into t1 values('a'),('b'),('c'),('d'), ('d')
 insert into t2 values('d'),('a'), ('e'),('a'),('d')
 
 select * from t2 where name not in (select distinct name from t1)
 
 select t1.name as t1, t2.name as t2 from t1 left join t2 on t1.name = t2.name where t2.name is null
 
 create table t3(name varchar(5), id int, pid int null)
 insert into t3 values('a',1,null),('b',2,1),('c',3,1),('d',4,null), ('e',4,3),('f',4,2);
 
 with g(name,id,pid, gr, le)
 as
 (
     select *, CAST(RAND(CHECKSUM(NEWID())) * 10 as INT) as gr, 0 as le from t3 where pid is null
     union all
     select t3.name,t3.id,t3.pid,g.gr, g.le +1 from t3 join g on t3.pid = g.id
    )
    
    select * from g
 
 
 select *, Rand(100) as gr from t3 where pid is not null
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 

