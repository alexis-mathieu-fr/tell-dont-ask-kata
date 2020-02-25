desc "Install bundler and then the gems for this kata"
task :install do
  exec("gem install bundler")
  exec("bundle install")
end

desc "Run test framework"
task :test do
  exec("rspec spec/use_cases/*")
end
