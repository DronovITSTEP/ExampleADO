/*1*/
select Countries.Name, Cities.Name, Cities.Population, PartOfTheWorld.Name from Countries
full join Cities on Countries.Id = Cities.CountryId
full join PartOfTheWorld on Countries.PartOfTheWorldId = PartOfTheWorld.Id
/*2*/
/*3*/
select Countries.Name, Cities.Name, Cities.Population, PartOfTheWorld.Name from Countries
join Cities on Countries.Id = Cities.CountryId
join PartOfTheWorld on Countries.PartOfTheWorldId = PartOfTheWorld.Id
where Countries.Name =/*@param*/'%';
/*4*/
select Cities.Name, Cities.Population from Cities
join Countries on Cities.CountryId = Countries.Id
where Countries.Name = /*@param*/'%';
/*5*/
select name from countries 
where name like /*@param*/'%';
/*6*/
select name from Cities 
join Capitals on Cities.Id = Capitals.CityId 
where Name like /*@param*/'%';
/*7*/
select top 3 c.Name from Cities c 
join Capitals cap on c.Id = cap.CityId 
order by c.Population;
/*8*/
select top 3 Countries.Name, sum(Cities.Population) from Cities 
join Countries on Cities.CountryId = Countries.Id
group by Countries.Name
order by sum(Cities.Population)
/*9*/
select pw.Name, AVG(Cities.Population) from PartOfTheWorld pw
join Countries on pw.Id = Countries.PartOfTheWorldId
join Capitals on Capitals.CountryId = Countries.Id
join Cities on Capitals.CityId = Cities.Id
group by pw.Name
/*10*/
select Countries.Name, PartOfTheWorld.Name, sum(Cities.Population) from Countries
join PartOfTheWorld on Countries.PartOfTheWorldId = PartOfTheWorld.Id
join Cities on Countries.Id = Cities.CountryId
group by Countries.Name, PartOfTheWorld.Name
/*11*/
select Countries.Name, PartOfTheWorld.Name, sum(Cities.Population) sumP from Countries
join PartOfTheWorld on Countries.PartOfTheWorldId = PartOfTheWorld.Id
join Cities on Countries.Id = Cities.CountryId
group by Countries.Name, PartOfTheWorld.Name
/*12*/
select Countries.Name, AVG(Cities.Population) from Countries
join Cities on Countries.Id = Cities.CountryId
where Countries.Name = /*@param*/'%'
group by Countries.Name
/*13*/
select Cities.Name, min(Cities.Population) from Cities
join Countries on Cities.CountryId = Countries.Id
where Countries.Name = /*@param*/'%'
group by Cities.Name
