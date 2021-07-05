function P = midP(p1,p2,f)
x = p1(1) * (1-f) + p2(1) * f;
y = p1(2) * (1-f) + p2(2) * f;
P = [x,y];
end